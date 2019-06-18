#define S0 4
#define S1 5
#define S2 6
#define S3 7
#define sensorOut 8
#define VCC 12

int whiteR = 230;
int whiteG = 250;
int whiteB = 200;
int blackR = 20;
int blackG = 20;
int blackB = 20;
boolean calibration = false;
boolean running = true;

void setup() {
  //TIMSK0= 0;
  pinMode(S0, OUTPUT);
  pinMode(S1, OUTPUT);
  pinMode(S2, OUTPUT);
  pinMode(S3, OUTPUT);
  pinMode(sensorOut, INPUT);
  pinMode(VCC, OUTPUT);
  
  reset();
  
  Serial.begin(9600);
}

void reset() {
  digitalWrite(VCC,HIGH);
  digitalWrite(S0,HIGH);
  digitalWrite(S1,LOW);
}

void loop() {
  int input = Serial.read();
  if (input != -1){
    switch(input){
      case 'o':
        reset();
        running = true;
      break;
      case 'f':
        running = false;
      break;
    }
  }

  if(running){
    digitalWrite(VCC,HIGH);
  
    digitalWrite(S2,LOW);
    digitalWrite(S3,LOW);
    int r = pulseIn(sensorOut, LOW);
    if(!calibration){
      r = map(r, whiteR, blackR, 0, 255);
      r = constrain(r, 0, 255);
    }
    delay(100); 
  
    digitalWrite(S2,HIGH);
    digitalWrite(S3,HIGH);
    int g = pulseIn(sensorOut, LOW);
    if(!calibration){
      g = map(g, whiteG, blackG, 0, 255);
      g = constrain(g, 0, 255);
    }
    delay(100);
    
    digitalWrite(S2,LOW);
    digitalWrite(S3,HIGH);
    int b = pulseIn(sensorOut, LOW);
    if(!calibration){
      b = map(b, whiteB, blackB, 0, 255);
      b = constrain(b, 0, 255);
    }
    delay(100);
  
    Serial.println(String(r) + "," + String(g) + "," + String(b) );
  }else{
    digitalWrite(S0,LOW);
    digitalWrite(S1,LOW);
    digitalWrite(S2,LOW);
    digitalWrite(S3,LOW);
    digitalWrite(VCC,LOW);
    
    Serial.println("");
  }

}
