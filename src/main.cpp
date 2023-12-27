#include <esp_log.h>
#include <wifi.h>
#include <display.h>
#include <tempSensor.h>
#include <driver/i2c.h>
#include <sys/time.h>

#define sda_pin 7
#define scl_pin 6
#define frequency 400000

void setup_i2c();
void data_render(void *pvParameters);
extern "C" {
    void app_main() {
        // Setup System Configuration
        setup_i2c();
        // Setup all the components
        display_init();
        hdc2080_init();
        setup_wlan();
        // Run component startup scripts
        display_write_page("Status", 0, true);
        // Run the main functions
        xTaskCreate(data_render, "data_render", configMINIMAL_STACK_SIZE+512, NULL, 5, NULL);
    }
}

// This ESP32 board does not have multiple I2C buses; thus, we share the it with the display and the sensor
void setup_i2c() {
    i2c_config_t conf;
    conf.mode = I2C_MODE_MASTER;
    conf.sda_io_num = sda_pin;
    conf.sda_pullup_en = GPIO_PULLUP_ENABLE;
    conf.scl_io_num = scl_pin;
    conf.scl_pullup_en = GPIO_PULLUP_ENABLE;
    conf.master.clk_speed = frequency;
    conf.clk_flags = 0;
    i2c_param_config(I2C_NUM_0, &conf);
    if(i2c_driver_install(I2C_NUM_0, I2C_MODE_MASTER, 0, 0, 0) == ESP_OK)
        ESP_LOGI("I2C_Setup", "hdc2080 driver installed successfully");
    else
        ESP_LOGE("I2C_Setup", "hdc2080 driver failed to install");
}

void data_render(void *pvParameters) {
    // Page 0 is the for header
    // Page 1 is for the wifi status
    // Page 2 is for system time
    // Page 3 is for the server connection status
    // Page 4 is for the HDC2080 temperature status
    // Page 5 is for the HDC2080 humidity status

    while(1) {
        // Render system time (MM/DD/YY HH:MM:SS)
        char timeStr[17];
        time_t now;
        time(&now);
        struct tm *timeinfo = localtime(&now);
        strftime(timeStr, sizeof(timeStr), "%m/%d/%y%H:%M:%S", timeinfo);
        display_write_page(timeStr, 2, false);

        // Render Temperature and stuff
        double temperature, humidity;
        esp_err_t ret = hdc2080_read_sensor(&temperature, &humidity);
        if(ret == ESP_OK) {
            // Report temperature
            char tempStr[17];
            sprintf(tempStr, "Temp: %.2fC", temperature);
            display_write_page(tempStr, 4, false);
            // Report humidity
            char humidityStr[17];
            sprintf(humidityStr, "Humid: %.2f%%", humidity);
            display_write_page(humidityStr, 5, false);
        } else {
            ESP_LOGE("main", "Failed to read sensor data");
            display_write_page("Temp: ERR", 4, false);
            display_write_page("Humid: ERR", 5, false);
        }
        vTaskDelay(pdMS_TO_TICKS(1000));
    }
}