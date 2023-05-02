using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RutaAutobus : MonoBehaviour
{
    Node[] pathNode;
    public GameObject coche;
    public float velocidadCoche;
    float tiempo;
    int actual;
    int anterior;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    float distancia;
    private globalVariables globalVariables;


    // Start is called before the first frame update

    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        actual = 0;
        tiempo = 0;
        pathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode(){
        tiempo=0;
        anterior = actual;
        actual++;
        if(actual == pathNode.Length){ 
            actual = 0; 
        }
        posicionAnterior = pathNode[anterior].transform.position;
        posicionActual = pathNode[actual].transform.position;
        coche.transform.LookAt(posicionActual);
        coche.transform.Rotate(0, -90, 0);
        distancia = Vector3.Distance(posicionAnterior,posicionActual);

    }

    // Update is called once per frame
    void Update(){
        tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
        
        if(coche.transform.position != posicionActual){
            coche.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
        }
        else{
            CheckNode();
        }
    }
}
