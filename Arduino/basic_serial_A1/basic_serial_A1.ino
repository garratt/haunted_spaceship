
int inByte = 0;        // incoming serial byte

void setup() {
  // start serial port at 9600 bps and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ;  // wait for serial port to connect. Needed for native USB port only
  }

  pinMode(8, INPUT);   // red button on pin 8
  digitalWrite(8,LOW);
  pinMode(10, INPUT);   // green button on pin 2
  digitalWrite(10,LOW);
  establishContact();  // send a byte to establish contact until receiver responds
}

void loop() {
  // if we get a valid byte, read analog ins:
  if (Serial.available() > 0) {
    // get incoming byte:
    inByte = Serial.read();
    // read first analog input:
    int pot = analogRead(A0);
    // read buttons:
    uint8_t button1 = digitalRead(8);
    uint8_t button2 = digitalRead(10);
    Serial.print("A1,D,");
    Serial.print(pot);
    Serial.print(",");
    Serial.print(button1);
    Serial.print(",");
    Serial.println(button2);
  }
}

void establishContact() {
  while (Serial.available() <= 0) {
    Serial.println("A1,C,Initls ../");  // send an initial string
    delay(300);
  }
}
