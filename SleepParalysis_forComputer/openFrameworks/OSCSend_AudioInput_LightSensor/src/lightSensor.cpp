//
//  lightSensor.cpp
//  week_13_05_petitPrince_05
//
//  Created by Gabriel Gianordoli on 11/16/13.
//
//

#include "lightSensor.h"

static io_connect_t dataPort = 0;
kern_return_t kr;

void lightSensor::setup(){
    serviceObject = IOServiceGetMatchingService(kIOMasterPortDefault, IOServiceMatching("AppleLMUController"));
    if (!serviceObject) {
        fprintf(stderr, "failed to find ambient light sensors\n");
    }
    
    kr = IOServiceOpen(serviceObject, mach_task_self(), 0, &dataPort);
    IOObjectRelease(serviceObject);
    if (kr != KERN_SUCCESS) {
        mach_error("IOServiceOpen:", kr);
    }
    
    setbuf(stdout, NULL);
    printf("%8ld %8ld", 0L, 0L);
}

//--------------------------------------------------------------
void lightSensor::update(){
    kern_return_t kr;
    uint32_t outputs = 2;
    uint64_t values[outputs];
    
    kr = IOConnectCallMethod(dataPort, 0, nil, 0, nil, 0, values, &outputs, nil, 0);
    if (kr == KERN_SUCCESS) {
        ambientLight = values[0];
        //        ofLogNotice() << values[0] << " ---- " << values[1];
    }
    
}

