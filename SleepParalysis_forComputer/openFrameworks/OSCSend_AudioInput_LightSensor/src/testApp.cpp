#include "testApp.h"

//--------------------------------------------------------------
void testApp::setup(){	 
	
	//osc send oF example referenced
	// open an outgoing connection to HOST:PORT
	sender.setup(HOST, PORT);
	
	//light sensor add-on created by Gabriel Gianordoli
	/*---------- LIGHT SENSOR ----------*/
    myLight.setup();
	
	//audio input oF example referenced
	ofSetVerticalSync(true);
	ofSetCircleResolution(80);
	ofBackground(54, 54, 54);	
	
	// 0 output channels, 
	// 2 input channels
	// 44100 samples per second
	// 256 samples per buffer
	// 4 num buffers (latency)
	
	soundStream.listDevices();
	
	//if you want to set a different device id 
	//soundStream.setDeviceID(0); //bear in mind the device id corresponds to all audio devices, including  input-only and output-only devices.
	
	int bufferSize = 256;
	
	
	left.assign(bufferSize, 0.0);
	right.assign(bufferSize, 0.0);
	volHistory.assign(400, 0.0);
	
	bufferCounter	= 0;
	drawCounter		= 0;
	smoothedVol     = 0.0;
	scaledVol		= 0.0;
	
	soundStream.setup(this, 0, 2, 44100, bufferSize, 4);
	
}

//--------------------------------------------------------------
void testApp::update(){
	
//light sensor
	myLight.update(); 
	
//audio
	//lets scale the vol up to a 0-1 range 
	scaledVol = ofMap(smoothedVol, 0.0, 0.17, 0.0, 1.0, true);
	
	//lets record the volume into an array
	volHistory.push_back( scaledVol );
	
	//if we are bigger the the size we want to record - lets drop the oldest value
	if( volHistory.size() >= 400 ){
		volHistory.erase(volHistory.begin(), volHistory.begin()+1);
	}
}

//--------------------------------------------------------------
void testApp::draw(){
	
//osc send
	// display instructions
	string buf;
	buf = "sending osc messages to" + string(HOST) + ofToString(PORT);
	ofDrawBitmapString(buf, 10, 20);
	ofDrawBitmapString("move the mouse to send osc message [/mouse/position <x> <y>]", 10, 50);
	ofDrawBitmapString("click to send osc message [/mouse/button <button> <\"up\"|\"down\">]", 10, 65);
	ofDrawBitmapString("press A to send osc message [/test 1 3.5 hello <time>]", 10, 80);

//light sensor
	scaledLight = ofMap(myLight.ambientLight, 0, 1000000, 0, 5);
	cout << myLight.ambientLight << endl;

//audio
ofSetColor(225);
ofDrawBitmapString("AUDIO INPUT EXAMPLE", 32, 32);
ofDrawBitmapString("press 's' to unpause the audio\n'e' to pause the audio", 31, 92);

ofNoFill();

// draw the left channel:
ofPushStyle();
ofPushMatrix();
ofTranslate(32, 170, 0);

ofSetColor(225);
ofDrawBitmapString("Left Channel", 4, 18);

ofSetLineWidth(1);	
ofRect(0, 0, 512, 200);

ofSetColor(245, 58, 135);
ofSetLineWidth(3);

ofBeginShape();
for (unsigned int i = 0; i < left.size(); i++){
	ofVertex(i*2, 100 -left[i]*180.0f);
}
ofEndShape(false);

ofPopMatrix();
ofPopStyle();

// draw the right channel:
ofPushStyle();
ofPushMatrix();
ofTranslate(32, 370, 0);

ofSetColor(225);
ofDrawBitmapString("Right Channel", 4, 18);

ofSetLineWidth(1);	
ofRect(0, 0, 512, 200);

ofSetColor(245, 58, 135);
ofSetLineWidth(3);

ofBeginShape();
for (unsigned int i = 0; i < right.size(); i++){
	ofVertex(i*2, 100 -right[i]*180.0f);
}
ofEndShape(false);

ofPopMatrix();
ofPopStyle();

// draw the average volume:
ofPushStyle();
ofPushMatrix();
ofTranslate(565, 170, 0);

	runningVol =scaledVol * 100.0;
ofSetColor(225);
ofDrawBitmapString("Scaled average vol (0-100): " + ofToString(runningVol, 0), 4, 18);
ofRect(0, 0, 400, 400);
	//cout << runningVol <<endl;

ofSetColor(245, 58, 135);
ofFill();		
ofCircle(200, 200, scaledVol * 190.0f);

//lets draw the volume history as a graph
ofBeginShape();
for (unsigned int i = 0; i < volHistory.size(); i++){
	if( i == 0 ) ofVertex(i, 400);
		
		ofVertex(i, 400 - volHistory[i] * 70);
		
		if( i == volHistory.size() -1 ) ofVertex(i, 400);
			}
ofEndShape(false);		

ofPopMatrix();
ofPopStyle();

drawCounter++;

ofSetColor(225);
string reportString = "buffers received: "+ofToString(bufferCounter)+"\ndraw routines called: "+ofToString(drawCounter)+"\nticks: " + ofToString(soundStream.getTickCount());
ofDrawBitmapString(reportString, 32, 589);

}

//--------------------------------------------------------------
void testApp::audioIn(float * input, int bufferSize, int nChannels){	
	
	//audio
	float curVol = 0.0;
	
	// samples are "interleaved"
	int numCounted = 0;	
	
	//lets go through each sample and calculate the root mean square which is a rough way to calculate volume	
	for (int i = 0; i < bufferSize; i++){
		left[i]		= input[i*2]*0.5;
		right[i]	= input[i*2+1]*0.5;
		
		curVol += left[i] * left[i];
		curVol += right[i] * right[i];
		numCounted+=2;
	}
	
	//this is how we get the mean of rms :) 
	curVol /= (float)numCounted;
	
	// this is how we get the root of rms :) 
	curVol = sqrt( curVol );
	
	smoothedVol *= 0.93;
	smoothedVol += 0.07 * curVol;
	
	bufferCounter++;
	
	//osc send
	//sends data to Unity's osc receiver
	ofxOscMessage m;
	m.setAddress("/audioInput");
	m.addIntArg(runningVol);
	m.addIntArg(scaledLight);
	
	
	cout << "runningVol:" << m.getArgAsInt32(0) << endl;
	cout << "scaledLight:" << m.getArgAsInt32(1) << endl;
	
	sender.sendMessage(m);
	
}

//--------------------------------------------------------------
void testApp::keyPressed  (int key){ 
	
	//osc send
	if(key == 'a' || key == 'A'){
		ofxOscMessage m;
		m.setAddress("/test");
		m.addIntArg(1);
		m.addFloatArg(3.5f);
		m.addStringArg("hello");
		m.addFloatArg(ofGetElapsedTimef());
		sender.sendMessage(m);
	}
	
	//audio
	if( key == 's' ){
		soundStream.start();
	}
	
	if( key == 'e' ){
		soundStream.stop();
	}
}

//--------------------------------------------------------------
void testApp::keyReleased(int key){ 
	
}

//--------------------------------------------------------------
void testApp::mouseMoved(int x, int y ){
	//osc send
	ofxOscMessage m;
	m.setAddress("/mouse/position");
	m.addIntArg(x);
	m.addIntArg(y);
	sender.sendMessage(m);
}

//--------------------------------------------------------------
void testApp::mouseDragged(int x, int y, int button){
	
}

//--------------------------------------------------------------
void testApp::mousePressed(int x, int y, int button){
	//osc send
	ofxOscMessage m;
	m.setAddress("/mouse/button");
	m.addStringArg("down");
	sender.sendMessage(m);
}

//--------------------------------------------------------------
void testApp::mouseReleased(int x, int y, int button){
	//osc send
	ofxOscMessage m;
	m.setAddress("/mouse/button");
	m.addStringArg("up");
	sender.sendMessage(m);
}

//--------------------------------------------------------------
void testApp::windowResized(int w, int h){
	
}

//--------------------------------------------------------------
void testApp::gotMessage(ofMessage msg){
	
}

//--------------------------------------------------------------
void testApp::dragEvent(ofDragInfo dragInfo){ 
	
}

