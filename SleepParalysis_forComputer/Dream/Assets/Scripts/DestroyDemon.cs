using UnityEngine;
using System.Collections;

public class DestroyDemon : MonoBehaviour {

	public Transform sleeperObject;
	public Transform sleeperObjectBody;

	// Use this for initialization
	void Start () {
//		sleeperObject = GameObject.Find("BullSkull").transform;	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	void OnTriggerEnter(Collider other) {
		//Debug.Log("yes!");

		//demon particles
		sleeperObject.particleSystem.Play();

		// Destroy demon
		Destroy(sleeperObject);
		Destroy(sleeperObjectBody);
		
	}
}
