#include <client.h>
#include <mqtt_client.h>
#include <esp_log.h>
#include <tempSensor.h>
#include <sys/time.h>
#include "pb_encode.h"
#include "pb_decode.h"
#include "queue.pb.h"

#define LOGTYPE "MQTT"
#define clientURL "mqtt://"

// Initalize the mqtt client
esp_mqtt_client_handle_t client;
void mqtt_init() {
    esp_mqtt_client_config_t mqtt_cfg = {
        .broker = {
            .address {
                .uri = clientURL
            },
        },
    };
    client = esp_mqtt_client_init(&mqtt_cfg);
    if(esp_mqtt_client_start(client) != ESP_OK) {
        ESP_LOGE(LOGTYPE, "MQTT client failed to start");
        return;
    }
    ESP_LOGE(LOGTYPE, "MQTT Client Successfully Connected...");
}

// String Encoder for protobuf strings type
bool encode_string(pb_ostream_t* stream, const pb_field_t* field, void* const* arg)
{
    const char* str = (const char*)(*arg);

    if (!pb_encode_tag_for_field(stream, field))
        return false;

    return pb_encode_string(stream, (uint8_t*)str, strlen(str));
}

// Setup the publisher to send data to the broker every 30 seconds
void mqtt_app_start(void *pvParameters) {
    const char *topic = "SensorRecord";
    int msg_id;
    while(1) {
        // Setup Protobuf payload
        uint8_t payload[256];
        pb_ostream_t stream = pb_ostream_from_buffer(payload, sizeof(payload));
        QueueData recordData = QueueData_init_zero;

        // Get current timestamp
        struct timeval tv;
        gettimeofday(&tv, NULL);
        char ms_time_str[21];
        uint64_t ms_time = (uint64_t)tv.tv_sec * 1000 + (uint64_t)tv.tv_usec / 1000;
        sprintf(ms_time_str, "%llu", ms_time);
        
        // Read the temperature data from the sensor
        hdc2080_read_sensor(&recordData.temperature, &recordData.humidity);

        recordData.timestamp.arg = (void*)ms_time_str;
        recordData.timestamp.funcs.encode = &encode_string;
        recordData.identifier.arg = (void*)"MyRoom";
        recordData.identifier.funcs.encode = &encode_string;

        if(!pb_encode(&stream, QueueData_fields, &recordData)) {
            ESP_LOGE(LOGTYPE, "Failed to encode data");
            vTaskDelay(5000 / portTICK_PERIOD_MS);
            continue;
        }

        // Publish the message to the broker
        msg_id = esp_mqtt_client_publish(client, topic, (char*)payload, stream.bytes_written, 2, 0);
        ESP_LOGI(LOGTYPE, "sent publish successful, msg_id=%d", msg_id);

        vTaskDelay(30000 / portTICK_PERIOD_MS);
    }
}