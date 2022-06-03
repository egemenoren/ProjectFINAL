#include <ArduinoJson.h>
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <DHT.h>

const char* ssid     = "Big-Mammas Salon 1";         // The SSID (name) of the Wi-Fi network you want to connect to
const char* password = "Magnolia";     // The password of the Wi-Fi network
#define DHTTYPE DHT11

const char* serverName = "https://kamupanel.online/api/getusersplants?userId=10";
unsigned long lastTime = 0;
unsigned long timerDelay = 5000;

String sensorReadings;
String humidityHistory;




char *plants = NULL;

void setup() {
  Serial.begin(115200);
  Serial.println("DHTxx test!");

  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
 
  Serial.println("Timer set to 5 seconds (timerDelay variable), it will take 5 seconds before publishing the first reading.");


}

void loop() {
  
if ((millis() - lastTime) > timerDelay) {


    
  if(WiFi.status()== WL_CONNECTED){
    sensorReadings = httpGETRequest(serverName);
    StaticJsonDocument<512> doc;
    deserializeJson(doc, sensorReadings);

    Serial.println(sensorReadings);
    //Serial.println(doc["list"].size());
    
    for(int i=0;i<doc["list"].size();i++){
      //#define DHTPIN (int)doc["list"][i]["HumidityCardAddress"]
      
      DHT dht((int)doc["list"][i]["HumidityCardAddress"], DHTTYPE);  
      Serial.println(doc["list"][i]["HumidityCardAddress"].as<String>());
      dht.begin();

      float h = dht.readHumidity();
      float t = dht.readTemperature();
      float f = dht.readTemperature(true);
    
      if (isnan(h) || isnan(t) || isnan(f)) {
        Serial.println("Failed to read from DHT sensor!");
        continue;
      }
    
      Serial.print("Humidity: ");
      Serial.print(h);
      Serial.print(" %\t");
      Serial.print("Temperature: ");
      Serial.print(t);
      Serial.println(" *C ");
      Serial.println("-------------------------------------");

      
      int plantId = doc["list"][i]["PlantId"];
      const char* HumidityCardAddress = doc["list"][i]["HumidityCardAddress"];
      const char* PumpCardAddress = doc["list"][i]["PumpCardAddress"];
      String serverNameHumidity = "https://kamupanel.online/api/gethumidityandtemperaturetodb?temperature="+String(t)+"&humidityrate="+String(h)+"&plantid="+String(plantId)+"";char Buf[50];
      char __serverNameHumidity[serverNameHumidity.length()+1];
      serverNameHumidity.toCharArray(__serverNameHumidity, sizeof(__serverNameHumidity));
      Serial.println(__serverNameHumidity);
      humidityHistory = httpGETRequest(__serverNameHumidity);
      StaticJsonDocument<256> docWatering;
      deserializeJson(docWatering, humidityHistory);
      Serial.println(humidityHistory);
      int waterTime = docWatering["waterTime"];
      bool isNeedWater = docWatering["isNeedWater"];
      int pumpAddress = docWatering["pumpAddress"];
      Serial.println(waterTime);
      Serial.println(isNeedWater);
      Serial.println(pumpAddress);

      if(isNeedWater == 1){
        pinMode(pumpAddress,OUTPUT);
        digitalWrite(pumpAddress,HIGH);  // Röleyi açık konuma getir
        delay(waterTime*1000);  // 3 saniye bekle
        digitalWrite(pumpAddress,LOW);  // Röleyi kapalı konuma getir

        Serial.println("Başarılı");
          
      }
      delay(500);
    }
         
  }else{
      Serial.println("WiFi Disconnected");
   }
    lastTime = millis();
  }

  
  delay(10000);

  
}

String httpGETRequest(const char* serverName) {
  WiFiClientSecure client;
  //WiFiClient client;
  HTTPClient http;

  client.setInsecure();
  http.begin(client, serverName);
  
  int httpResponseCode = http.GET();
  
  String payload = "{}"; 
  
  if (httpResponseCode>0) {
    payload = http.getString();
  }
  else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  http.end();

  return payload;
}
