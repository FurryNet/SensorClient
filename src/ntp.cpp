#include <ntp.h>
#include <esp_log.h>
#include "esp_sntp.h"

#define TAG "NTP"

// Sync the system time with NTP
void sync_systime() {

    // Initialize the NTP client
    ESP_LOGI(TAG, "Initializing SNTP...");
    esp_sntp_setoperatingmode(SNTP_OPMODE_POLL);
    sntp_set_sync_mode(SNTP_SYNC_MODE_IMMED);
    esp_sntp_setservername(0, "pool.ntp.org");
    esp_sntp_init();

    // Get the NTP time and update it
    while(1) {
        time_t now = 0;
        struct tm timeinfo = { 0 };
        time(&now);
        localtime_r(&now, &timeinfo);
        if(timeinfo.tm_year > (2016 - 1900)) {
            ESP_LOGI(TAG, "Time Synced");
            break;
        }
        ESP_LOGI(TAG, "Waiting for time sync...");
        vTaskDelay(pdMS_TO_TICKS(2000));
    }
    setenv("TZ", "UTC", 1);

    // Disconnect from the NTP server
    ESP_LOGI(TAG, "Sync Complete...");
    esp_sntp_stop();
}