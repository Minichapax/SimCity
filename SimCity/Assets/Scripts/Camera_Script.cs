using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {

	private float moveSpeed = 10.0f;
	private float zoomSpeed = 7.0f; 

	private Vector3 lastMousePosition;
	private Quaternion originalRotation; // variable que guarda la rotación original


	void Start()
	{
		lastMousePosition = Input.mousePosition;
		originalRotation = transform.rotation; // guarda la rotación original

	}
	
	void LateUpdate()
	{
		Vector3 mousePosition = Input.mousePosition;
		Vector3 delta = mousePosition - lastMousePosition;
		delta *= moveSpeed * Time.deltaTime;

		float zoom = Input.GetAxis("Mouse ScrollWheel");
		transform.Translate(0, 0, zoom*zoomSpeed);

		if (Input.GetMouseButton(0))
		{
			Quaternion originalRotationTemp = transform.rotation; // guarda la rotación actual de la cámara
			transform.rotation = originalRotation;
			transform.Translate(-delta.x, -delta.y, 0);
			transform.rotation = originalRotationTemp;
		}

		if (Input.GetMouseButton(1))
		{			
			transform.Rotate(-delta.y , 0, 0);
		}

		lastMousePosition = mousePosition;
	}

	

}   
