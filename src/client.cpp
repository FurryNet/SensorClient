#include <client.h>
#include <mqtt_client.h>
#include <esp_log.h>
#include <errno.h>
#include <tempSensor.h>
#include <display.h>
#include <sys/time.h>
#include "pb_encode.h"
#include "pb_decode.h"
#include "queue.pb.h"

#define LOGTYPE "MQTT"

// Root certificate for the broker (Let's Encrypt R3)
const char* ROOT_CERT = "-----BEGIN CERTIFICATE-----\n\
MIIFFjCCAv6gAwIBAgIRAJErCErPDBinU/bWLiWnX1owDQYJKoZIhvcNAQELBQAw\n\
TzELMAkGA1UEBhMCVVMxKTAnBgNVBAoTIEludGVybmV0IFNlY3VyaXR5IFJlc2Vh\n\
cmNoIEdyb3VwMRUwEwYDVQQDEwxJU1JHIFJvb3QgWDEwHhcNMjAwOTA0MDAwMDAw\n\
WhcNMjUwOTE1MTYwMDAwWjAyMQswCQYDVQQGEwJVUzEWMBQGA1UEChMNTGV0J3Mg\n\
RW5jcnlwdDELMAkGA1UEAxMCUjMwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEK\n\
AoIBAQC7AhUozPaglNMPEuyNVZLD+ILxmaZ6QoinXSaqtSu5xUyxr45r+XXIo9cP\n\
R5QUVTVXjJ6oojkZ9YI8QqlObvU7wy7bjcCwXPNZOOftz2nwWgsbvsCUJCWH+jdx\n\
sxPnHKzhm+/b5DtFUkWWqcFTzjTIUu61ru2P3mBw4qVUq7ZtDpelQDRrK9O8Zutm\n\
NHz6a4uPVymZ+DAXXbpyb/uBxa3Shlg9F8fnCbvxK/eG3MHacV3URuPMrSXBiLxg\n\
Z3Vms/EY96Jc5lP/Ooi2R6X/ExjqmAl3P51T+c8B5fWmcBcUr2Ok/5mzk53cU6cG\n\
/kiFHaFpriV1uxPMUgP17VGhi9sVAgMBAAGjggEIMIIBBDAOBgNVHQ8BAf8EBAMC\n\
AYYwHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMBMBIGA1UdEwEB/wQIMAYB\n\
Af8CAQAwHQYDVR0OBBYEFBQusxe3WFbLrlAJQOYfr52LFMLGMB8GA1UdIwQYMBaA\n\
FHm0WeZ7tuXkAXOACIjIGlj26ZtuMDIGCCsGAQUFBwEBBCYwJDAiBggrBgEFBQcw\n\
AoYWaHR0cDovL3gxLmkubGVuY3Iub3JnLzAnBgNVHR8EIDAeMBygGqAYhhZodHRw\n\
Oi8veDEuYy5sZW5jci5vcmcvMCIGA1UdIAQbMBkwCAYGZ4EMAQIBMA0GCysGAQQB\n\
gt8TAQEBMA0GCSqGSIb3DQEBCwUAA4ICAQCFyk5HPqP3hUSFvNVneLKYY611TR6W\n\
PTNlclQtgaDqw+34IL9fzLdwALduO/ZelN7kIJ+m74uyA+eitRY8kc607TkC53wl\n\
ikfmZW4/RvTZ8M6UK+5UzhK8jCdLuMGYL6KvzXGRSgi3yLgjewQtCPkIVz6D2QQz\n\
CkcheAmCJ8MqyJu5zlzyZMjAvnnAT45tRAxekrsu94sQ4egdRCnbWSDtY7kh+BIm\n\
lJNXoB1lBMEKIq4QDUOXoRgffuDghje1WrG9ML+Hbisq/yFOGwXD9RiX8F6sw6W4\n\
avAuvDszue5L3sz85K+EC4Y/wFVDNvZo4TYXao6Z0f+lQKc0t8DQYzk1OXVu8rp2\n\
yJMC6alLbBfODALZvYH7n7do1AZls4I9d1P4jnkDrQoxB3UqQ9hVl3LEKQ73xF1O\n\
yK5GhDDX8oVfGKF5u+decIsH4YaTw7mP3GFxJSqv3+0lUFJoi5Lc5da149p90Ids\n\
hCExroL1+7mryIkXPeFM5TgO9r0rvZaBFOvV2z0gp35Z0+L4WPlbuEjN/lxPFin+\n\
HlUjr8gRsI3qfJOQFy/9rKIJR0Y/8Omwt/8oTWgy1mdeHmmjk7j1nYsvC9JSQ6Zv\n\
MldlTTKB3zhThV1+XWYp6rjd5JW1zbVWEkLNxE7GJThEUG3szgBVGP7pSWTUTsqX\n\
nLRbwHOoq7hHwg==\n\
-----END CERTIFICATE-----\n";


static void mqtt_event_handler(void *handler_args, esp_event_base_t base, int32_t event_id, void *event_data) {
    esp_mqtt_event_handle_t event = (esp_mqtt_event_handle_t)event_data;
    // esp_mqtt_client_handle_t client = event->client;
    // your_context_t *context = event->context;

    switch (event->event_id) {
        case MQTT_EVENT_BEFORE_CONNECT:
            ESP_LOGI(LOGTYPE, "MQTT_EVENT_BEFORE_CONNECT");
            display_write_page("MQTT: PreCon", 3, false);
            break;
        case MQTT_EVENT_CONNECTED:
            ESP_LOGI(LOGTYPE, "MQTT Broker Connected");
            display_write_page("MQTT: Conn", 3, false);
            break;
        case MQTT_EVENT_DISCONNECTED:
            ESP_LOGI(LOGTYPE, "MQTT_EVENT_DISCONNECTED");
            display_write_page("MQTT: Discon", 3, false);
            break;
        case MQTT_EVENT_ERROR:
            char dMsg[17]; // Display Message
            switch(event->error_handle->error_type) {
                case MQTT_ERROR_TYPE_TCP_TRANSPORT:
                    ESP_LOGE(LOGTYPE, "MQTT TCP Transport Error: %d", event->error_handle->esp_transport_sock_errno);
                    sprintf(dMsg, "MQTT_TCPErr: %d", event->error_handle->esp_transport_sock_errno);
                    display_write_page(dMsg, 3, false);
                    break;
                case MQTT_ERROR_TYPE_CONNECTION_REFUSED:
                    ESP_LOGE(LOGTYPE, "MQTT Connection Refused: %d", event->error_handle->connect_return_code);
                    sprintf(dMsg, "MQTT_ConRef: %d", event->error_handle->connect_return_code);
                    display_write_page(dMsg, 3, false);
                    break;
                default:
                    ESP_LOGE(LOGTYPE, "MQTT Unknown Error: %d", event->error_handle->error_type);
                    display_write_page("MQTT: UnknownErr", 3, false);
                    break;
            }
            break;
        default:
            break;
    }
}


// Initalize the mqtt client
esp_mqtt_client_handle_t client;
void mqtt_init() {
    display_write_page("MQTT: Discon", 3, false);
    esp_mqtt_client_config_t mqtt_cfg = {
        .broker = {
            .address {
                .uri = "NULL"
            },
            .verification = {
                .certificate = ROOT_CERT,
            }
        },
        .credentials = {
            .username = "NULL",
            .authentication = {
                .password = "NULL",
            }
        }
    };
    client = esp_mqtt_client_init(&mqtt_cfg);

    // Register the event handler
    esp_err_t evntrgr = esp_mqtt_client_register_event(client, MQTT_EVENT_ANY, mqtt_event_handler, client);
    if(evntrgr != ESP_OK) {
        display_write_page("MQTT: Unknown", 3, false);
        ESP_LOGE(LOGTYPE, "MQTT client failed to register event handler: %d", evntrgr);
    }
    if(esp_mqtt_client_start(client) != ESP_OK) {
        display_write_page("MQTT: Failed", 3, false);
        ESP_LOGE(LOGTYPE, "MQTT client failed to start: %d", errno);
        return;
    }
    ESP_LOGI(LOGTYPE, "MQTT Client Started");

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
    int msg_id = -2;

    // Wait for a second to collect enough samples
    if(msg_id == -2)
        vTaskDelay(1000 / portTICK_PERIOD_MS);
    
    while(1) {
        // Setup Protobuf payload
        uint8_t payload[256];
        pb_ostream_t stream = pb_ostream_from_buffer(payload, sizeof(payload));
        QueueData recordData = QueueData_init_zero;

        // Get current timestamp
        struct timeval tv;
        gettimeofday(&tv, NULL);
        recordData.timestamp = (uint64_t)tv.tv_sec * 1000 + (uint64_t)tv.tv_usec / 1000;
        
        // Read the temperature data from the sensor
        hdc2080_read_sensor(&recordData.temperature, &recordData.humidity);

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

        vTaskDelay(10000 / portTICK_PERIOD_MS);
    }
}