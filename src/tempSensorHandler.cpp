#include <tempSensor.h>
#include <driver/i2c.h>
#include <esp_log.h>

#define TAG "tempSensor"

// define the pins used by the i2c bus
#define int_pin 20

// HDC2080 Register Addresses
#define HDC2080_ADDR 0x40
#define HDC2080_TEMP_LOW_REG  0x00
#define HDC2080_TEMP_HIGH_REG 0x01
#define HDC2080_HUM_LOW_REG 0x02
#define HDC2080_HUM_HIGH_REG 0x03
#define HDC2080_CONFIG_REG 0x0F
#define HDC2080_INIT_CONFIG_REG 0x0E

// Read the first byte of the specified register
esp_err_t reg_read(uint8_t reg, uint8_t *data) {
    i2c_cmd_handle_t cmd = i2c_cmd_link_create();
    i2c_master_start(cmd);
    i2c_master_write_byte(cmd, (HDC2080_ADDR << 1) | I2C_MASTER_WRITE, true);
    i2c_master_write_byte(cmd, reg, true);
    i2c_master_start(cmd);
    i2c_master_write_byte(cmd, (HDC2080_ADDR << 1) | I2C_MASTER_READ, true);
    i2c_master_read_byte(cmd, data, I2C_MASTER_NACK);
    i2c_master_stop(cmd);
    esp_err_t ret = i2c_master_cmd_begin(I2C_NUM_0, cmd, 1000 / portTICK_PERIOD_MS);


    i2c_cmd_link_delete(cmd);
    if (ret != ESP_OK) {
        ESP_LOGE(TAG, "Failed to read byte from register");
        return ret;
    }
    return ESP_OK;
}

// Write a byte to the specified register
esp_err_t reg_write(uint8_t reg, uint8_t data) {
    i2c_cmd_handle_t cmd = i2c_cmd_link_create();
    i2c_master_start(cmd);
    i2c_master_write_byte(cmd, (HDC2080_ADDR << 1) | I2C_MASTER_WRITE, true);
    i2c_master_write_byte(cmd, reg, true);
    i2c_master_write_byte(cmd, data, true);
    i2c_master_stop(cmd);
    esp_err_t ret = i2c_master_cmd_begin(I2C_NUM_0, cmd, 1000 / portTICK_PERIOD_MS);
    i2c_cmd_link_delete(cmd);
    if (ret != ESP_OK) {
        ESP_LOGE(TAG, "Failed to write byte to register");
        return ret;
    }
    return ESP_OK;
}

// Reset the sensor data
void reset_sensor() {
    reg_write(HDC2080_INIT_CONFIG_REG, 0x80);
    vTaskDelay(pdMS_TO_TICKS(50));
}

// Configure the sensor
void hdc2080_init() {
    reset_sensor();
    uint8_t currentData;

    /* INIT CONFIGURATION */
    reg_read(HDC2080_INIT_CONFIG_REG, &currentData);
    // Set auto measurement mode to 5Hz (setting bit 6:4 to 111)
    currentData |= (1 << 6);
    currentData |= (1 << 5);
    currentData |= (1 << 4);
    reg_write(HDC2080_INIT_CONFIG_REG, currentData);

    /* MEASUREMENT CONFIGURATION */
    reg_read(HDC2080_CONFIG_REG, &currentData);
    // // Set temp and humidity resolution to 14 bits (setting bit 7:6 to 00)
    // currentData &= ~(1 << 7);
    // currentData &= ~(1 << 6);
    // // Set measurement mode to both temperature and humidity (setting bit 2:1 to 00)
    // currentData &= ~(1 << 2);
    // currentData &= ~(1 << 1);
    // Trigger measurement on both temp and humidity (setting bit 0 to 1)
    currentData |= 1;
    // Write the new configuration to the sensor
    reg_write(HDC2080_CONFIG_REG, currentData);


}

// Thii function reads the temperature and humidity data from the sensor
esp_err_t hdc2080_read_sensor(double *temperature, double *humidity) {
    // Read temperature data
    uint8_t data[2];

    esp_err_t low_temp_ret = reg_read(HDC2080_TEMP_LOW_REG, &data[0]);
    esp_err_t high_temp_ret = reg_read(HDC2080_TEMP_HIGH_REG, &data[1]);
     if (low_temp_ret != ESP_OK) {
         ESP_LOGE(TAG, "Failed to read low temperature data");
         return low_temp_ret;
     }
    if (high_temp_ret != ESP_OK) {
        ESP_LOGE(TAG, "Failed to read high temperature data");
        return high_temp_ret;
    }
    uint16_t temp_raw = (data[1] << 8) | data[0];
    *temperature = (double)temp_raw / 65536.0 * 165.0 - (40.0+ 0.08*(3.3-1.8));

    // Read humidity data

    esp_err_t low_hum_ret = reg_read(HDC2080_HUM_LOW_REG, &data[0]);
    esp_err_t high_hum_ret = reg_read(HDC2080_HUM_HIGH_REG, &data[1]);
    if (low_hum_ret != ESP_OK) {
        ESP_LOGE(TAG, "Failed to read low humidity data");
        return low_hum_ret;
    }
    if (high_hum_ret != ESP_OK) {
        ESP_LOGE(TAG, "Failed to read high humidity data");
        return high_hum_ret;
    }

    uint16_t hum_raw = (data[1] << 8) | data[0];
    *humidity = (double)hum_raw / 65536.0 * 100.0;
    
    // Reconfigure the sensor if the data is back to default
    if((*temperature) < -40 || (*humidity) == 0)
        hdc2080_init();
    return ESP_OK;
}
