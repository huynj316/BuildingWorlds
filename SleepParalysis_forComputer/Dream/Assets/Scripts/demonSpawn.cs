using UnityEngine;
using System.Collections;

public class demonSpawn : MonoBehaviour {

	
	public Transform demon; // assign in inspector
	public float x;
	public float y;
	public float z;
	
	public int demonSpawned;
	
	float nextDemonTime = 0f; // the time, in seconds, when I should plant again
	
	void Start () {
		nextDemonTime = Time.time + 2f; // when I am born, set my next plant time later
		
	}
	
	// Update is called once per frame
	void Update () {
		//		int cubeSoFar = 0;
		
		//		if (cubeSoFar <= 5) {
		// if it is time to plant, then...
		//time number in sec when u start playing
		if (Time.time > nextDemonTime && demonSpawned < 5) {
			if (Random.Range (0f, 1f) > 0.1f ) { // 90% chance of planting a tree, every frame
				Instantiate (demon, new Vector3(x,y,z), Quaternion.Euler (78,0,0));
				//				cubeSoFar++;
				demonSpawned++;
				x = Random.Range(-.3f,.5f);
					
				z = Random.Range (-.1f, -1f);
			} 
			nextDemonTime += Random.Range( 5f, 10f); // set the next planting time
			//			}	
		}
	}
}
