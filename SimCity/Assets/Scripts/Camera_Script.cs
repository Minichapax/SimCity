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
			transform.Translate(-delta.x, 0 , -delta.y);
			transform.rotation = originalRotationTemp;
		}

		if (Input.GetMouseButton(1))
		{			
			transform.Rotate(-delta.y , 0, 0);
		}

		lastMousePosition = mousePosition;
	

		if(transform.position.x < -30){
			transform.position = new Vector3(-30, transform.position.y, transform.position.z);
		}
		if(transform.position.x > 30){
			transform.position = new Vector3(30, transform.position.y, transform.position.z);
		}
		if(transform.position.y < 5){
			transform.position = new Vector3(transform.position.x,5, transform.position.z);
		}
		if(transform.position.y > 60){
			transform.position = new Vector3(transform.position.x, 60, transform.position.z);
		}
		if(transform.position.z < -30){
			transform.position = new Vector3(transform.position.x, transform.position.y, -30);
		}
		if(transform.position.z > 30){
			transform.position = new Vector3(transform.position.x, transform.position.y, 30);
		}
		Debug.Log(transform.rotation.eulerAngles);
		if(transform.rotation.eulerAngles.y > 0 ){ 
			transform.rotation = Quaternion.Euler(90, 0, 0);
		}
		if(transform.rotation.eulerAngles.x < 5){ 
			transform.rotation = Quaternion.Euler(5, 0, 0);
		}
	}
}   
