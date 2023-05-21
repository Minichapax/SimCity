using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;

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
    public RutaPeaton peatonETSE;   
    public RutaPeaton1 peatonFutbol;
    public RutaPeaton2 peatonConchi;
    public RutaPeaton3 peatonParking;
    public RutaPeaton4 peatonFisica; 
    public RutaPeaton5 peatonCondesa;
    
    public RutaPeatonSaliendo peatonSaliendoEmprendia;
    public RutaPeatonSaliendo1 peatonSaliendoPsico;
    public RutaPeatonSaliendo2 peatonSaliendoQuimica;
    public RutaPeatonSaliendo3 peatonSaliendoMates;
    public RutaPeatonSaliendo4 peatonSaliendoDerecho;
    public RutaPeatonSaliendo5 peatonSaliendoFarmacia;


    private bool parada;
    private float tiempoParada;


    // Start is called before the first frame update
    static NpgsqlConnection GetConnection()
    {
        string connectionString = "Host=localhost;Username=alumnogreibd;Password=greibd2015;Database=campusInteligente";
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        return connection;
    }

    private void insertParadas(int parada){
        using (NpgsqlConnection connection = GetConnection())
        {
            connection.Open();
            string queryInsert = "INSERT INTO parada_transporte VALUES (1, @Parada, @Fecha)";
            string paradastring = "Parada 1";
            using (NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection))
            {
                switch(parada){
                    case 1:
                        paradastring = "Parada 1";
                    break;
                    case 6:
                        paradastring = "Parada 2";
                    break;
                    case 16:
                        paradastring = "Parada 3";
                    break;
                    case 20:
                        paradastring = "Parada 4";
                    break;
                    case 27:
                        paradastring = "Parada 5";
                    break;
                    case 31:
                        paradastring = "Parada 6";
                    break;
                    default:
                        paradastring = "Parada unknown";
                    break;
                }
                command.Parameters.AddWithValue("@Parada", paradastring);
                command.Parameters.AddWithValue("@Fecha", globalVariables.getDia());
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

    } 

    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        actual = 3;
        tiempo = 0;
        pathNode = GetComponentsInChildren<Node>();
        coche.SetActive(false);
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
            insertParadas(actual);
            if(actual == 1){
                peatonETSE.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoEmprendia.busToggle();
            }
            if(actual == 6){
                peatonFisica.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoPsico.busToggle();
            }
            if(actual == 16){
                peatonCondesa.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoFarmacia.busToggle();
            }
            if(actual == 20){
                peatonParking.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoQuimica.busToggle();
            }
            if(actual == 27){
                peatonConchi.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoDerecho.busToggle();
            }
            if(actual == 31){
                peatonFutbol.busToggle();
                if(Random.Range(1,4) == 1) peatonSaliendoMates.busToggle();
            }
        }
    }

    // Update is called once per frame
    void Update(){
        
        if(globalVariables.getHoraDelDia()  >= 82800 || globalVariables.getHoraDelDia() < 23400){
                coche.SetActive(false);
            }else{
                coche.SetActive(true);
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
                }
            }
    }
}
