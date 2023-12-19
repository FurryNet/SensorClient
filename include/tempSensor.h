
#include <esp_err.h>

void hdc2080_init();
esp_err_t hdc2080_read_sensor(double *temperature, double *humidity);