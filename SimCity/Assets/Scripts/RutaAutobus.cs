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
    private bool parada;
    private float tiempoParada;


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
        if(actual == 1 || actual == 6 || actual == 16 || actual == 20 || actual == 27 || actual == 31){
            parada = true;
            tiempoParada = 0.0f;
        }
    }

    // Update is called once per frame
    void Update(){
        if(parada){
            tiempoParada += Time.deltaTime*globalVariables.velocidadDia;
            if(tiempoParada > 15.0f) parada = false;

        }else{
            tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
            
            if(coche.transform.position != posicionActual){
                coche.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
            }
            else{
                CheckNode();
            }
            if(globalVariables.getHoraDelDia()  >= 82800 || globalVariables.getHoraDelDia() < 23400){
                coche.SetActive(false);
            }else{
                coche.SetActive(true);
            }
        }
    }
}
