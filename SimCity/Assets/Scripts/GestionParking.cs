using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;

public class GestionParking : MonoBehaviour
{
    public PanelCanvasMulta panel;
    public GameObject cochesGrupo;
    public GameObject rutaEntrada;
    public GameObject rutaSalida;
    public float velocidad;
    public Texture icono;
    public Texture iconoNoti;
    public GameObject boton;

    private bool[] enParking = new bool[5];
    private DatosEstacionamiento[] datosParking= new DatosEstacionamiento[5];
    private string[] multa = new string[5];
    private float[] tiemposParking = new float[5];
    private Node[] nodosEntrada;
    private Node[] nodosSalida;
    private Coche[] coches;
    private int cocheUso;
    private bool cocheMovimiento=false;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    private float tiempo;
    private float distancia;
    private int num_nodoEntrada=0;
    private int num_nodoSalida=0;
    private int entradaSalida=0;
    private globalVariables globalVariables;
    private bool open=false;
    private RawImage rawImage;
    private float tiempoHora=0;
    // Start is called before the first frame update

    static NpgsqlConnection GetConnection()
    {
        string connectionString = "Host=localhost;Username=alumnogreibd;Password=greibd2015;Database=campusInteligente";
         NpgsqlConnection connection = new NpgsqlConnection(connectionString);
         return connection;
    }

    void Start()
    {
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        nodosEntrada = rutaEntrada.GetComponentsInChildren<Node>();
        nodosSalida = rutaSalida.GetComponentsInChildren<Node>();
        coches = cochesGrupo.GetComponentsInChildren<Coche>();
        for(int i = 0; i<coches.Length; i++){
            coches[i].setActive(false);
        }

        for(int i = 0; i<multa.Length; i++){
            multa[i]="";
        }

        rawImage = boton.GetComponent<RawImage>();
    }

    private void registrarEntrada(){
        coches[cocheUso].setActive(false);
        enParking[cocheUso] = true;
        datosParking[cocheUso].tiempoReal = Random.Range(3600, 10800);
        datosParking[cocheUso].tiempoEstacionamiento = Random.Range(3600, 12600);
    }

    private void comprobarSalida(){
        float diferencia = -1;
        if(datosParking[cocheUso].tiempoEstacionamiento < datosParking[cocheUso].tiempoReal){
            diferencia = (datosParking[cocheUso].tiempoReal - datosParking[cocheUso].tiempoEstacionamiento)/60;
            string textoMulta = "El coche "+ coches[cocheUso].getMatricula() + " se ha pasado " + diferencia.ToString("F2") + " minutos.\n";
            multa[cocheUso] = textoMulta;
        }
        cocheMovimiento=false;

        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        
        using (NpgsqlConnection connection = GetConnection())
        {
            connection.Open();
            string queryInsert = "INSERT INTO coche (matricula, fecha_y_hora_de_entrada, fecha_y_hora_de_salida,multa,  tiempo_pagado, id_parking )"+
                                "VALUES (@coche, @entrada, @salida, @multa, @tiempo, 1)"+
                                "ON CONFLICT (matricula) DO UPDATE " +
								"set fecha_y_hora_de_entrada = @entrada, " +
								"fecha_y_hora_de_salida = @salida, " +
								"tiempo_pagado = @tiempo, " +
                                "multa = @multa;";

            using (NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection))
             {
                command.Parameters.AddWithValue("@coche", coches[cocheUso].getMatricula());
                command.Parameters.AddWithValue("@entrada", globalVariables.getDia().AddMinutes(-datosParking[cocheUso].tiempoEstacionamiento));
                command.Parameters.AddWithValue("@salida", globalVariables.getDia());
                bool multa = false;
                if(diferencia > 0) multa = true;
                command.Parameters.AddWithValue("@multa", multa);
                command.Parameters.AddWithValue("@tiempo", datosParking[cocheUso].tiempoReal/60);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }   

    private void CheckNodeEntrada(){
        tiempo=0;
        posicionAnterior = nodosEntrada[num_nodoEntrada].transform.position;
        num_nodoEntrada++;
        if(num_nodoEntrada == nodosEntrada.Length){
            num_nodoEntrada=0;
            registrarEntrada();
            cocheMovimiento=false;
        }
        else{
            posicionActual=nodosEntrada[num_nodoEntrada].transform.position;
            coches[cocheUso].transform.LookAt(posicionActual);
            coches[cocheUso].transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
        }
    }

    private void CheckNodeSalida(){
        tiempo=0;
        posicionAnterior = nodosSalida[num_nodoSalida].transform.position;
        num_nodoSalida++;
        if(num_nodoSalida == nodosSalida.Length){
            num_nodoSalida=0;
            comprobarSalida();
        }
        else{
            posicionActual=nodosSalida[num_nodoSalida].transform.position;
            coches[cocheUso].transform.LookAt(posicionActual);
            coches[cocheUso].transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
        }
    }

    private bool hayMultas(){
        for(int i = 0; i<multa.Length; i++){
            if(multa[i] != "")
                return true;
        }
        return false;
    }

    private string getTextoMultas(){
        string resultado="";
        if(hayMultas()){
            resultado+="Multas sin consultar:\n";
            for(int i = 0; i<multa.Length; i++){
                if(multa[i] != ""){
                    resultado+=multa[i];
                    multa[i]="";
                }
            }
        }
        else{
            resultado="No hay nuevas multas.";
        }
        return resultado;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoHora += Time.deltaTime * globalVariables.velocidadDia;
        if(tiempoHora > 3600){
            tiempoHora=0;
            if(hayMultas())
                rawImage.texture=iconoNoti;
        }

        if(!cocheMovimiento){
            cocheUso = Random.Range(0, coches.Length);
            if(!enParking[cocheUso]){
                entradaSalida=0;
                tiemposParking[cocheUso]=0;
                num_nodoEntrada=0;
                cocheMovimiento=true;
                coches[cocheUso].transform.position = nodosEntrada[0].transform.position;
                coches[cocheUso].setActive(true);
                CheckNodeEntrada();
            }
            else if(enParking[cocheUso] && tiemposParking[cocheUso] > datosParking[cocheUso].tiempoReal){
                tiemposParking[cocheUso] = 0;
                enParking[cocheUso] = false;
                cocheMovimiento=true;
                entradaSalida=1;
                num_nodoSalida=0;
                coches[cocheUso].transform.position = nodosSalida[0].transform.position;
                coches[cocheUso].setActive(true);
                CheckNodeSalida();
            }
        }
        
        if(cocheMovimiento){
            tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidad/ (distancia*15);
            if(coches[cocheUso].transform.position != posicionActual){
                coches[cocheUso].transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
            }
            else{
                if(entradaSalida == 0)
                    CheckNodeEntrada();
                else
                    CheckNodeSalida();
            }
        }

        for(int j = 0; j<tiemposParking.Length;j++){
            if(enParking[j]){
                tiemposParking[j] += Time.deltaTime * globalVariables.velocidadDia;
            }
        }
    }

    public void botonParking(){
        if(open){
            panel.setActive(false);
            open=false;
        }
        else{
            panel.UpdateTexts(getTextoMultas());
            if(rawImage.texture == iconoNoti)
                rawImage.texture = icono;
            panel.setActive(true);
            open=true;
        }
    }
}
