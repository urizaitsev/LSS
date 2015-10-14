#include <Wire.h>
#include <Adafruit_ADS1015.h>
#include <Adafruit_MCP4725.h>

Adafruit_ADS1115 ads1115;
Adafruit_MCP4725 dac;

void setup(void)
{
  Serial.begin(9600);
  Serial.println("Hello!");
  
  Serial.println("Single-ended readings from AIN0 with >3.0V comparator");
  Serial.println("ADC Range: +/- 6.144V (1 bit = 3mV)");
  Serial.println("Comparator Threshold: 1000 (3.000V)");
  ads1115.begin();
  ads1115.setGain(GAIN_TWOTHIRDS);
    // For Adafruit MCP4725A1 the address is 0x62 (default) or 0x63 (ADDR pin tied to VCC)
  // For MCP4725A0 the address is 0x60 or 0x61
  // For MCP4725A2 the address is 0x64 or 0x65
  dac.begin(0x62);
}

void loop(void)
{
  int16_t adc0;
  dac.setVoltage(4095, false);
  adc0 = ads1115.readADC_SingleEnded(0);
  Serial.print("AIN0: "); Serial.println(adc0);
  
  delay(100);
}
