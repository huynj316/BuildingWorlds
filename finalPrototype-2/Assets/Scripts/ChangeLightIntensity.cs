using UnityEngine;
using System.Collections;

public class ChangeLightIntensity : MonoBehaviour {
	public float amplitude;
	public Transform DemonSkin;
	public Material invisible;
	public Material stone;
	// Use this for initialization
	void Start () {
		amplitude = 0.2f;
		DemonSkin.renderer.material = invisible;
		
	}
	
	// Update is called once per frame
	void Update () {

		light.intensity = amplitude;

		if ( amplitude > 2f ) {

			DemonSkin.renderer.material = stone;
		}
	}
}