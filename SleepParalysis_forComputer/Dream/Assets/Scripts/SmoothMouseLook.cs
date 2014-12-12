using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public bool freeX = true;
	public bool freeY = false;
	
	public float minimumX = -180F;
	public float maximumX = 180F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationX = 0F;
	float rotationY = 0F;
	
	List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;  
	
	List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
	
	public float frameCounter = 20;
	
	Quaternion originalRotation;
	
	void Update ()
	{
		if (axes == RotationAxes.MouseXAndY)
		{          
			rotAverageY = 0f;
			rotAverageX = 0f;
			
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationX += Input.GetAxis ("Mouse X") * sensitivityX;
			
			if (!freeY)
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			if (!freeX)
				rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
			
			rotArrayY.Add(rotationY);
			rotArrayX.Add(rotationX);
			
			if (rotArrayY.Count >= frameCounter)
				rotArrayY.RemoveAt(0);
			if (rotArrayX.Count >= frameCounter)
				rotArrayX.RemoveAt(0);
			
			foreach (float rotY in rotArrayY)
				rotAverageY += rotY;
			foreach (float rotX in rotArrayX)
				rotAverageX += rotX;
			
			rotAverageY /= rotArrayY.Count;
			rotAverageX /= rotArrayX.Count;
			
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{          
			rotAverageX = 0f;
			
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			
			if (!freeX)
				rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
			
			rotArrayX.Add(rotationX);
			
			if (rotArrayX.Count >= frameCounter)
				rotArrayX.RemoveAt(0);
			
			foreach (float rotX in rotArrayX)
				rotAverageX += rotX;
			
			rotAverageX /= rotArrayX.Count;
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;          
		}
		else
		{          
			rotAverageY = 0f;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			
			if (!freeY)
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			rotArrayY.Add(rotationY);
			
			if (rotArrayY.Count >= frameCounter)
				rotArrayY.RemoveAt(0);
			
			foreach (float rotY in rotArrayY)
				rotAverageY += rotY;
			
			rotAverageY /= rotArrayY.Count;
			
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}

	}
	
	void Start ()
	{          
		if (rigidbody)
			rigidbody.freezeRotation = true;
		originalRotation = transform.localRotation;
	}
}