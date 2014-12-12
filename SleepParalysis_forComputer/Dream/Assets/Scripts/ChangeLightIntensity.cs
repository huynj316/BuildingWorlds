using UnityEngine;
using System.Collections;

public class ChangeLightIntensity : MonoBehaviour {
	public float amplitude;
	public float spotIncrease;

	// Use this for initialization
	void Start () {
		light.intensity = 0.5f;
		amplitude = 0.2f;

		
	}
	
	// Update is called once per frame
	void Update () {
		amplitude = UnityOSCListener.lightingScale;
		spotIncrease = UnityOSCListener.blowScale;
		light.spotAngle = spotIncrease;
		light.intensity = amplitude;


	}
}