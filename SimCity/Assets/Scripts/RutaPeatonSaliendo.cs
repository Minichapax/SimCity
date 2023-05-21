using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RutaPeatonSaliendo : MonoBehaviour
{
    // Start is called before the first frame update
    saliendoNode[] pathNode;
    float tiempo;
    int actual;
    int anterior;
    public float velocidadCoche;
    public GameObject peaton;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    float distancia;
    private globalVariables globalVariables;
    private bool parada;
    private float tiempoParada;
    private float randomParada;

    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        actual = 0;
        tiempo = 0;
        peaton.SetActive(false);
        pathNode = GetComponentsInChildren<saliendoNode>();
        CheckNode();
    }

    void CheckNode(){
        
        tiempo=0;
        anterior = actual;
        actual++;

        if(actual == pathNode.Length){ 
            actual = 0;
            peaton.SetActive(false);
        }
        if(actual == 1) parada = true;
        posicionAnterior = pathNode[anterior].transform.position;
        posicionActual = pathNode[actual].transform.position;
        peaton.transform.LookAt(posicionActual);
        distancia = Vector3.Distance(posicionAnterior,posicionActual);
        
    }

    // Update is called once per frame
    void Update(){
        if(globalVariables.getHoraDelDia()  >= 82800 || globalVariables.getHoraDelDia() < 23400){
            peaton.SetActive(false);
        }else{                
            if(!parada){
                tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
                
                if(peaton.transform.position != posicionActual){
                    peaton.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
                }
                else{
                    CheckNode();
                }
            }
        }
        
    }

    public void busToggle(){
        if(actual == 1){
            peaton.SetActive(true);
            parada = false;
        }
    }
}
