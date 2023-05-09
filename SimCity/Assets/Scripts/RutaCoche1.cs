using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RutaCoche1 : MonoBehaviour
{
    Node[] pathNode;
    private GameObject coche;
    public GameObject coche1;
    public GameObject coche2;
    public GameObject coche3;
    public GameObject coche4;
    public GameObject coche5;

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
    private float randomParada;


    // Start is called before the first frame update

    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        actual = 0;
        tiempo = 0;
        pathNode = GetComponentsInChildren<Node>();
        coche1.SetActive(false);
        coche2.SetActive(false);
        coche3.SetActive(false);
        coche4.SetActive(false);
        coche5.SetActive(false);
        CheckNode();
    }

    void CheckNode(){
        

        tiempo=0;
        anterior = actual;
        actual++;

        if(anterior == 0){
            int cochelere = Random.Range(1, 6);
            if (cochelere == 1) coche = coche1;
            if (cochelere == 2) coche = coche2;
            if (cochelere == 3) coche = coche3;
            if (cochelere == 4) coche = coche4;
            if (cochelere == 5) coche = coche5;
        }

        if(actual == pathNode.Length){ 
            actual = 0;
        }
        posicionAnterior = pathNode[anterior].transform.position;
        posicionActual = pathNode[actual].transform.position;
        coche.transform.LookAt(posicionActual);
        coche.transform.Rotate(0, -90, 0);
        distancia = Vector3.Distance(posicionAnterior,posicionActual);
        if(actual == 0){
            parada = true;
            tiempoParada = 0.0f;
            randomParada = Random.Range(50f, 100f);
            coche.SetActive(false);
        }
        if(anterior == 0){
            coche.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update(){
        if(parada){
            tiempoParada += Time.deltaTime*globalVariables.velocidadDia;
            if(tiempoParada > randomParada) parada = false;

        }else{
            tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
            
            if(coche.transform.position != posicionActual){
                coche.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
            }
            else{
                CheckNode();
            }
        
        }
    }
}
