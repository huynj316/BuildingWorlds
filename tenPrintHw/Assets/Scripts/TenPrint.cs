using UnityEngine;
using System.Collections;

public class TenPrint : MonoBehaviour {
	
	public Transform Building1;
	public Transform Building2;
	public Transform Building3;
	public Transform Food;
	public AudioClip box;

	void Start () {

		StartCoroutine (TenPrintProcess());//coroutine can stop at points
	}
	
	// Update is called once per frame // will happen continually
//	void Update () { //"gluing strings together" = concatenation
	IEnumerator TenPrintProcess () {
		int cubeSoFar = 0; // how many characters I've printed so far in this line
		float x = 0;
		float y = 0;
		float z = 0;

		while (true) {
			//flip virtual coin, if heads print \ if tales print /
			if(Random.Range(0f,10f)<5f){ //5 give equal probability
				Instantiate (Building1, new Vector3(x,0,0), Quaternion.Euler (0,45,0)); //rotate from 0-360
				x++;
				cubeSoFar++;
				audio.PlayOneShot (box);

			} else {
				Instantiate (Building2, new Vector3(0,0,z), Random.rotation);
				z++;
			}
				cubeSoFar++;
			if (cubeSoFar >= 10) { //after 10 slashes
				Instantiate (Food, new Vector3(0,y,0), Random.rotation);
				y++;
				cubeSoFar = 0;
				} else {
				Instantiate (Building3, new Vector3(0,0,(z+5)), Random.rotation);
			}
			yield return new WaitForSeconds(0.1f); //tell unity to take a break
			
		}
	}
}
