using UnityEngine;
using System.Collections;

public class GenerateDemons : MonoBehaviour {
	
//	public Transform demon;
//	
//
//	void Start () {
//		
//		StartCoroutine (DemonGenerator());//coroutine can stop at points
//	}
//
//	IEnumerator DemonGenerator () {
//		int cubeSoFar = 0; // how many characters I've printed so far in this line
//		float x = 0;
//		float y = 0;
//		float z = 0;
//		
//		while (true) {
//			//flip virtual coin, if heads print \ if tales print /
//				if (cubeSoFar <= 5) {
//			if(Random.Range(0f,5f)<3f){ //5 give equal probability
//				Instantiate (demon, new Vector3(x,0,0), Quaternion.Euler (0,45,0)); //rotate from 0-360
//				x++;
//				cubeSoFar++;
////				audio.PlayOneShot (box);
////				if (demon 
//				
////			} 
////				else {
////				Instantiate (demon, new Vector3(0,0,z), Random.rotation);
////				z++;
////			}
////			cubeSoFar++;
////			if (cubeSoFar >= 10) { //after 10 slashes
////				Instantiate (demon, new Vector3(0,y,0), Random.rotation);
////				y++;
////				cubeSoFar = 0;
////			} else {
////				Instantiate (demon, new Vector3(0,0,(z+5)), Random.rotation);
////			}
//			yield return new WaitForSeconds(0.1f); //tell unity to take a break
//			}
//				}
//		}
//	}

	public Transform demon; // assign in inspector
	public float x;
	public float y;
	public float z;
	
	float nextDemonTime = 0f; // the time, in seconds, when I should plant again
	
	void Start () {
		nextDemonTime = Time.time + 2f; // when I am born, set my next plant time later
	}
	
	// Update is called once per frame
	void Update () {
		int cubeSoFar = 0;

		if (cubeSoFar <= 5) {
		// if it is time to plant, then...
		if (Time.time > nextDemonTime ) {
			if (Random.Range (0f, 1f) > 0.1f ) { // 90% chance of planting a tree
				Instantiate ( demon, new Vector3(x,y,z), Quaternion.Euler (0,45,0));
				cubeSoFar++;
				x++;
				z++;
			} 
			nextDemonTime += Random.Range( 5f, 10f); // set the next planting time
			}	
		}
	}
}
