#include <esp_log.h>
#include <wifi.h>
#include <display.h>
#include <tempSensor.h>
#include <sys/time.h>

void data_render(void *pvParameters);
extern "C" {
    void app_main() {
        // Setup all the components
        display_init();
        hdc2080_init();
        setup_wlan();
        // Run component startup scripts
        display_write_page("Status", 0, true);
        // Run the main functions
        xTaskCreate(data_render, "data_render", 4096, NULL, 5, NULL);
    }
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