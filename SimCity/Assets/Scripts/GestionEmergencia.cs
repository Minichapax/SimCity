using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionEmergencia : MonoBehaviour
{

    public Light luz;
    public GameObject emergencia;
    public Edificio emergenciaETSE;
    public GameObject rutaETSE;
    public GameObject rutaDER;
    public GameObject rutaFARM;
    public GameObject rutaPSICO;
    public float velocidadCoche;
    public GameObject edificios;

    private Node[] rutaemerETSE;
    private Emergencia[] edificiosemer;
    private globalVariables globalVariables;
    private Light_Script sol;
    private int num_nodo;
    private bool emergenciaActiva;
    private bool emergenciaActivadahoy;
    private float distancia;
    private float tiempo;
    private float tiempoemergencia=0;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    private Node[] rutaemergen;
    private Emergencia emergenciaEdificio;

    // Start is called before the first frame update
    void Start()
    {
        edificiosemer=edificios.GetComponentsInChildren<Emergencia>();
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        sol=GameObject.Find("Sol").GetComponent<Light_Script>();
        emergenciaActiva = false;
        emergenciaActivadahoy = false;
    }

    void CheckNode(){
        tiempo=0;
        posicionAnterior=rutaemergen[num_nodo].transform.position;
        num_nodo++;
        if(num_nodo == rutaemergen.Length){ 
            num_nodo = 0;
            emergenciaActiva=false; 
        }
        else{
            posicionActual=rutaemergen[num_nodo].transform.position;
            emergencia.transform.LookAt(posicionActual);
            emergencia.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
        }
    }

    void SelectEmergencia(){
        int randomIndex = Random.Range(0, edificiosemer.Length);
        emergenciaEdificio=edificiosemer[randomIndex%edificiosemer.Length];
        if(edificiosemer[randomIndex].name == "DERECHO"){
            rutaemergen=rutaDER.GetComponentsInChildren<Node>();
        }
        else if(edificiosemer[randomIndex].name == "ETSE"){
            rutaemergen=rutaETSE.GetComponentsInChildren<Node>();
        }
        else if(edificiosemer[randomIndex].name == "PSICO"){
            rutaemergen=rutaPSICO.GetComponentsInChildren<Node>();
        }
        else if(edificiosemer[randomIndex].name == "Farmacia"){
            rutaemergen=rutaFARM.GetComponentsInChildren<Node>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(globalVariables.getHoraDelDia() > 40000 && emergenciaActiva == false && emergenciaActivadahoy == false){
            //Debug.Log(globalVariables.getHoraDelDiaHHMMSS());
            tiempoemergencia=0;
            sol.ToggleEmergencia(true);
            emergenciaActivadahoy = true;
            SelectEmergencia();
            luz.transform.position=emergenciaEdificio.transform.position + new Vector3(0,5,0);
            luz.enabled=true;
            emergencia.SetActive(true);
            emergenciaActiva=true;
            num_nodo=0;
            emergencia.transform.position=rutaemergen[0].transform.position;
            CheckNode();
        }

        if(emergenciaActiva){
            tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
            if(emergencia.transform.position != posicionActual){
                emergencia.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
            }
            else{
                CheckNode();
            }
        }

        if(luz.enabled == true && emergenciaActiva == false){
            //Debug.Log(globalVariables.getHoraDelDiaHHMMSS());
            tiempoemergencia+=Time.deltaTime * globalVariables.velocidadDia;
            if(tiempoemergencia > 1200){ 
                sol.ToggleEmergencia(false);
                emergencia.SetActive(false);
                luz.enabled=false;
            }
        }

        if(globalVariables.getHoraDelDia() < 40000){
            emergenciaActivadahoy = false;
        }
    }
}
