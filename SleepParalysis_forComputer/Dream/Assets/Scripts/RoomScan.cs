using UnityEngine;
using System.Collections;

public class RoomScan : MonoBehaviour {
	

	public GameObject putItHere;
	public GameObject putItHeadHere;
//	public GameObject putLightHere;
	public Material invisible;
	public Material stone;

	public AudioClip demon_call;
	public AudioClip explosion;

	
	public static Collider collider1 = new Collider();
	public Transform particle;
	public float amplitude;
	public float scaleIncrease;
	float intensity;

//	bool myLight;

	//uncomment when public static send is fixed
//	public float scaledIncrease;
//	public float ease;
//	float demonIncrease;


	
	// Use this for initialization
	void Start () {
			
		renderer.material = invisible;
		putItHeadHere.renderer.material = invisible;

		scaleIncrease = 0.05f;

		 
//		Light myLight = light.GetComponent("Light");
//		myLight.enabled = !myLight.enabled;
		//uncomment when public static send is fixed
//		scaledIncrease = 0.05f;


	}
	
	// Update is called once per frame
	void Update () {
		
		
		//ray = an origin (vector3) and direction (vector3)
		
		Debug.Log ("raycasting is working");
		Ray ray = Camera.main.ScreenPointToRay (new Vector2(640/2, 800/2));
		
			
		//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);//(camera.pixelWidth / 2), (camera.pixelHeight / 2));//Input.mousePosition); //initialize ray 
		//Ray ray = transform.position;
		
		//Ray ray = Camera.main.ScreenPointToRay (Camera.main.transform.position);//(camera.pixelWidth / 2), (camera.pixelHeight / 2));//Input.mousePosition); //initialize ray 
		RaycastHit rayHit = new RaycastHit ();//blank container for info
		
		//actually shoot the raycast now
		
		//if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, 1000f))
		//if (Physics.Raycast (Camera.main.transform.position, out rayHit, 1000f))
		Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.yellow);


		amplitude = UnityOSCListener.lightingScale;
		Debug.Log (amplitude);


		//if (Physics.Raycast (ray, out rayHit, 1000f))
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out rayHit, 1000f))
		{
			//Debug.DrawRay(ray, Camera.main.transform.forward * rayHit.distance);
			Debug.Log("RAYCASTING");	
			collider1 = rayHit.collider;
			Debug.Log(collider1.name);



			if (collider1.name == "demonBody" 
//			    && spotRange > 1 
			    ){
//				startCarl = true;

				audio.PlayOneShot (demon_call, Time.time);
				renderer.material = stone;
				putItHeadHere.renderer.material = stone;
				putItHeadHere.transform.localScale *= 1.01f;


				//uncomment when public static send is fixed
//				scaledIncrease = UnityOSCListener.demonScale;
//				demonIncrease = Mathf.Lerp(scaleIncrease, (scaleIncrease/60), Time.deltaTime*ease);
//				putItHeadHere.transform.localScale *= scaledIncrease*.001f;
//				Debug.Log (scaledIncrease);

				bool particleTriggered = false;


				if ( putItHeadHere.transform.localScale.x > 1.6f && !particleTriggered  ) {


					putItHeadHere.renderer.material = invisible;
					putItHere.renderer.material = invisible;
					particleTriggered = true;
					Instantiate(particle);
					if (particleTriggered == true && !light.enabled) {
						GetComponent<AudioSource>().clip =  explosion;
						audio.PlayOneShot (explosion, Time.time);
//						light.enabled = true;
					}
					Destroy(putItHere, 5f);


				}

//				renderer.material.color.a = (0.5);
				
				Debug.Log ("this is when it would initiate box animation before transitioning");

			}  else  {
				renderer.material = invisible;
				putItHeadHere.renderer.material = invisible;
				if ( putItHeadHere.transform.localScale.x > 4f ) {
					putItHeadHere.transform.localScale /= 1.01f;

					//uncomment when public static send is fixed
//					putItHeadHere.transform.localScale /= scaledIncrease/.001f;
				}
			}

		
		
		} 

	
}

}