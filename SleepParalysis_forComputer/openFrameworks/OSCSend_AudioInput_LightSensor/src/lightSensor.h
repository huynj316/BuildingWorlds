//
//  lightSensor.h
//  week_13_05_petitPrince_05
//
//  Created by Gabriel Gianordoli on 11/16/13.
//
//

#pragma once
#include "ofMain.h"
#include <mach/mach.h>
#import <IOKit/IOKitLib.h>
#import <CoreFoundation/CoreFoundation.h>

class lightSensor{
public:
    
    //functions
    void setup();
    void update();
    
    io_service_t serviceObject;
    int ambientLight;
    
};