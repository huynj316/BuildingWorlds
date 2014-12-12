using UnityEngine;
using System.Collections;

public class moveCarl : MonoBehaviour {
	public Transform demon; // assign in inspector
	public float x;
	public float y;
	public float z;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (x,y,z);
		x = Random.Range(-.3f,.5f);
		
		z = Random.Range (-.1f, -1f);
	}
}
