using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour {

	private float moveSpeed = 10.0f;
	private float zoomSpeed = 7.0f; 
	public GameObject edificios;
	private Vector3 lastMousePosition;
	private Quaternion originalRotation; // variable que guarda la rotación original
	public bool canMoveCamera = true;
	public bool canMoveCamera2 = true;
	private Edificio[] pathEdificios;
	private int numeroCamera = 0; 
	private Vector3 ultimaPosicion;
	private Quaternion ultimaRotation;
	void Start()
	{
		lastMousePosition = Input.mousePosition;
		originalRotation = transform.rotation; // guarda la rotación original
		pathEdificios = edificios.GetComponentsInChildren<Edificio>();

	}
	
	void LateUpdate()
	{
		if(canMoveCamera && canMoveCamera2){
			Vector3 mousePosition = Input.mousePosition;
			Vector3 delta = mousePosition - lastMousePosition;
			delta *= moveSpeed * Time.deltaTime;

			float zoom = Input.GetAxis("Mouse ScrollWheel");
			Quaternion zoomOriginalRotationTemp = transform.rotation; // guarda la rotación actual de la cámara
			transform.rotation = originalRotation;
			transform.Translate(0,0, zoom*zoomSpeed);
			transform.rotation = zoomOriginalRotationTemp;

			if (Input.GetMouseButton(0))
			{
				Quaternion originalRotationTemp = transform.rotation; // guarda la rotación actual de la cámara
				transform.rotation = originalRotation;
				transform.Translate(-delta.x, -delta.y , 0);
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
			if(transform.rotation.eulerAngles.y > 0 ){ 
				transform.rotation = Quaternion.Euler(90, 0, 0);
			}
			if(transform.rotation.eulerAngles.x < 5){ 
				transform.rotation = Quaternion.Euler(5, 0, 0);
			}
		}
	}

	public void botonCamera(){

			if(numeroCamera == 0){
				ultimaPosicion = transform.position;
				ultimaRotation = transform.rotation;
				canMoveCamera2 = false;
			}

			if(numeroCamera == pathEdificios.Length-1){
				canMoveCamera2 = true;
				transform.position = ultimaPosicion;
				transform.rotation = ultimaRotation;
				numeroCamera = 0;
			}else{

				Vector3 direccionObjeto = pathEdificios[numeroCamera].transform.forward;
			
				direccionObjeto = Vector3.Normalize(direccionObjeto);
				Vector3 posicionCamara = pathEdificios[numeroCamera].transform.position + direccionObjeto * 5f;
				posicionCamara.y+=4;
				transform.position = posicionCamara;
				transform.LookAt(pathEdificios[numeroCamera].transform.position);
				numeroCamera += 1;
			}
	}
}   
