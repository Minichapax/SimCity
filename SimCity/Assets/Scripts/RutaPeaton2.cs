using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RutaPeaton2 : MonoBehaviour
{
    // Start is called before the first frame update
    PeatonNode[] pathNode;
    float tiempo;
    int actual;
    int anterior;
    public float velocidadCoche;
    public GameObject peaton;
    private Animator animator;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    float distancia;
    private globalVariables globalVariables;
    private bool parada;
    private float tiempoParada;
    private float randomParada;
    private bool bus = false;
    private float tiempoespera = 0;
    private float tiempoesperarandom = 0;

    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        actual = 0;
        tiempo = 0;
        pathNode = GetComponentsInChildren<PeatonNode>();
        animator = peaton.GetComponent<Animator>();
        CheckNode();
    }

    void CheckNode(){
        
        tiempo=0;
        anterior = actual;
        actual++;

        if(actual == pathNode.Length){ 
            parada = true;
            actual = 0;
            animator.SetTrigger("esperar");
        }
        posicionAnterior = pathNode[anterior].transform.position;
        posicionActual = pathNode[actual].transform.position;
        if(actual != 0) peaton.transform.LookAt(posicionActual);
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
            if(bus){
                if(peaton.transform.position != posicionActual){
                    tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
                    peaton.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
                }else{
                    peaton.SetActive(true);
                    tiempoespera+=Time.deltaTime * globalVariables.velocidadDia;
                    if(tiempoespera > tiempoesperarandom){
                        bus = false;
                        parada = false;
                        animator.SetTrigger("andar");
                        CheckNode();
                    }
                }
            }
        }
        
    }

    public void busToggle(){
        peaton.SetActive(false);
        bus = true;
        tiempoesperarandom = Random.Range(300f, 900f);
    }
}
