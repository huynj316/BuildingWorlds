#pragma once

#include "ofMain.h"
#include "ofxOsc.h"
#include "lightSensor.h"

#define HOST "localhost"
#define PORT 12345

class testApp : public ofBaseApp{
	
public:
	
	void setup();
	void update();
	void draw();
	
	void keyPressed(int key);
	void keyReleased(int key);
	void mouseMoved(int x, int y );
	void mouseDragged(int x, int y, int button);
	void mousePressed(int x, int y, int button);
	void mouseReleased(int x, int y, int button);
	void windowResized(int w, int h);
	void dragEvent(ofDragInfo dragInfo);
	void gotMessage(ofMessage msg);
	
	//osc send
	ofTrueTypeFont font;
	ofxOscSender sender;
	
	//light sensor
	lightSensor myLight;
	float scaledLight;
	
	//audio
	void audioIn(float * input, int bufferSize, int nChannels); 
	
	vector <float> left;
	vector <float> right;
	vector <float> volHistory;
	
	int 	bufferCounter;
	int 	drawCounter;
	
	float smoothedVol;
	float scaledVol;
	int runningVol;
	
	ofSoundStream soundStream;
};
