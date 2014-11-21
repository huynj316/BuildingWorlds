using UnityEngine;
using System.Collections;

public class ChangeScale : MonoBehaviour {
//	public Transform putDemonHere;
	public float scaleIncrease;
	public Transform allDemon;
	public Material invisible;
	public Material stone;
	//	float intensity;

	// Use this for initialization
	void Start () {
//		putDemonHere.transform.localScale = 1f;
		scaleIncrease = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//		putDemonHere.transform.localScale *= scaleIncrease;

//		GameObject light = GameObject.Find ("Directional Light");
//		light.GetComponent<ChangeLightIntensity> ().amplitude = intensity;
//		if (intensity > 4.0f && intensity <14.0f) {

		scaleIncrease = UnityOSCListener.blowScale;
		transform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
		renderer.material = invisible;
//		} else {
//			scaleIncrease = 0.5f;
//		}
		if ( allDemon.transform.localScale.x > 4 ) {
			Destroy(allDemon);
//			renderer.material = invisible;
		}
//
//		Debug.Log (intensity);
	}
}
