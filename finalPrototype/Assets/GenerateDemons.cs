using UnityEngine;
using System.Collections;

public class GenerateDemons : MonoBehaviour {
	
	public Transform demonPrefab;

//	public AudioClip box;

	float nextDemonTime = 0f; // the time, in seconds, when I should plant again
	
	void Start () {
		nextDemonTime = Time.time + 5f; // when I am born, set my next plant time later
//		StartCoroutine (DemonGenerator());//coroutine can stop at points
	}

	void Update () {
		if (Time.time > nextDemonTime ) {
			if (Random.Range (-5f, 5f) > 0.5f ) { // 90% chance of planting a tree
				Instantiate ( demonPrefab, transform.position, Quaternion.identity );

			}
		}
	}
}
	// Update is called once per frame // will happen continually
	//	void Update () { //"gluing strings together" = concatenation
//	IEnumerator DemonGenerator () {
//		int cubeSoFar = 0; // how many characters I've printed so far in this line
//		float x = 0;
//		float y = 0;
//		float z = 0;
//		
//		while (true) {
//			//flip virtual coin, if heads print \ if tales print /
//			if(Random.Range(0f,5f)<3f){ //5 give equal probability
//				Instantiate (demon, new Vector3(x,0,0), Quaternion.Euler (0,45,0)); //rotate from 0-360
//				x++;
//				cubeSoFar++;
////				audio.PlayOneShot (box);
//				if (demon 
//				
//			} 
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
//			
//		}
//	}
