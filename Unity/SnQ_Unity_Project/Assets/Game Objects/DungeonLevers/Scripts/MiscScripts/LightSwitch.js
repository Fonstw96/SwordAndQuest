#pragma strict
var myObject : Transform[]; //assign all the lights you wish to control with this switch
var maxIntensity : float = 1; //the intesnsity of the light
var speed : float = 2; //how fast the lights turn on and off
private var lightsOn = true; //whether lights are on or off
private var endLoop = true; //stop loop

function Start() {
	//check if lights are on or off
	if(myObject[0]) {
		if(myObject[0].GetComponent(Light)) {
			if(myObject[0].GetComponent(Light).enabled == true) {
				lightsOn = true;
			}
			else {
				lightsOn = false;
			}
		}
		else {
			Debug.LogWarning("This is not a Light! You have assigned a GameObject without a Light component to the LightSwitch script.");
		}
	}
}
//THIS FUNCTION IS CALLED FROM OUR ACTIVATION-SCRIPT, VIA SEND-MESSAGE("ACTIVATE");
function Activate() {
	endLoop = true;
	yield;
	
	//determin wheather we should turn lights on or off
	if(lightsOn == true) {
		if(myObject[0].GetComponent(Light).intensity > 0) {
			lightsOn = false;
			//Call LightOff() Coroutine
			LightsOff();
		}
	}
	else {
		if(myObject[0].GetComponent(Light).intensity < maxIntensity) {
			lightsOn = true;
			//Call LightsOn() Coroutine
			LightsOn();
		}
	}
}
//Fade the Light intensity until the lights are off
function LightsOff() {
	endLoop = false;
	while(endLoop == false) {
		//turn all lights off
		for(var children in myObject) {
			var child : Transform = children as Transform;
			child.GetComponent(Light).intensity -= speed * Time.deltaTime;
			if(child.GetComponent(Light).intensity <= 0) {
				endLoop = true;
			}
		}
		yield;
	}
}
//Increase the Light intensity until the lights are fully on
function LightsOn() {
	endLoop = false;
	while(endLoop == false) {
		//turn all lights on
		for(var children in myObject) {
			var child : Transform = children as Transform;
			child.GetComponent(Light).intensity += speed * Time.deltaTime;
			if(child.GetComponent(Light).intensity >= maxIntensity) {
				endLoop = true;
			}
		}
		yield;
	}
}