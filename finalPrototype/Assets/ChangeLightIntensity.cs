using UnityEngine;
using System.Collections;

public class ChangeLightIntensity : MonoBehaviour {
	public float amplitude;
	// Use this for initialization
	void Start () {
		amplitude = 6f;
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity = amplitude;
	}
}
