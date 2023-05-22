using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Npgsql;

public class Edificio : MonoBehaviour
{
    public string Nombre;
    public string Calle;
    private static string Campus = "Campus Sur";
    private float ConsumoLuz;
    private float ConsumoAgua;
    private float ConsumoGas;
    private float Contaminacion;
    private float Temperatura;
    private DateTime fecha;
    
    private bool cocheCerca = false;
    private float time = 0f;
    private globalVariables globalVariables;


    // static NpgsqlConnection GetConnection()
    // {
    //     string connectionString = "Host=localhost;Username=alumnogreibd;Password=greibd2015;Database=campusInteligente";
    //     NpgsqlConnection connection = new NpgsqlConnection(connectionString);
    //     return connection;
    // }

    public string getString(){
        string text = "Edificio: "+ Nombre +
        "\nCalle: " + Calle +
        "\nCampus: " + Campus +
        "\nContaminacion: "+Contaminacion.ToString("F2")+
        "\nConsumoLuz: "+ConsumoLuz.ToString("F2")+ "w/h" +
        "\nConsumoAgua: "+ConsumoAgua.ToString("F2")+"l/h" +
        "\nConsumoGas: "+ConsumoGas.ToString("F2")+"l/h" +
        "\nTemperatura: "+Temperatura.ToString("F1")+ "ºC";
        return text;
    }
    void Start(){
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        

        // using (NpgsqlConnection connection = GetConnection())
        // {
        //     connection.Open();
        //     string queryInsert = "INSERT INTO edificio (nombre, calle, nombre_campus )"+
        //                         "VALUES (@Nombre, @Calle, @Campus)"+
        //                         "ON CONFLICT DO NOTHING";

        //     using (NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection))
        //     {
        //         command.Parameters.AddWithValue("@Nombre", Nombre);
        //         command.Parameters.AddWithValue("@Calle", Calle);
        //         command.Parameters.AddWithValue("@Campus", Campus);
        //         command.ExecuteNonQuery();
        //     }
        //     connection.Close();
        // }
    }   

    private void updateValues(){
        fecha = globalVariables.getDia();
        Contaminacion = UnityEngine.Random.Range(50f, 55f);// + cocheCerca;
        if(cocheCerca){ 
            Contaminacion += UnityEngine.Random.Range(30f, 40f);
            //Debug.Log(gameObject.name + Contaminacion);
        }
        ConsumoLuz = UnityEngine.Random.Range(200f, 210f);//cambiar a w/h
        ConsumoAgua = UnityEngine.Random.Range(160f, 170f);//l/h
        ConsumoGas = UnityEngine.Random.Range(230f, 240f);//l/h
        Temperatura  = UnityEngine.Random.Range(15f, 19f);
        if(globalVariables.getHoraDelDia()  >= 82800 || globalVariables.getHoraDelDia() < 23400){
            Temperatura  = UnityEngine.Random.Range(10f, 14f);
        }
    }

    public void insertSensores()
    {
        // using (NpgsqlConnection connection = GetConnection())
        // {
        //     connection.Open();
        //     string queryInsert = "INSERT INTO mediciones_edificio (nombre_edificio, contaminacion, fecha_y_hora_contaminacion, consumo_luz, fecha_y_hora_luz, consumo_agua, fecha_y_hora_agua, consumo_gas, fecha_y_hora_gas, temperatura, fecha_y_hora_temperatura)"+
        //                         "VALUES (@Nombre, @Contaminacion, @Fecha, @ConsumoLuz, @Fecha, @ConsumoAgua, @Fecha, @ConsumoGas, @Fecha, @Temperatura, @Fecha)";

        //     using (NpgsqlCommand command = new NpgsqlCommand(queryInsert, connection))
        //     {
        //         command.Parameters.AddWithValue("@Nombre", Nombre);
        //         command.Parameters.AddWithValue("@Contaminacion", Contaminacion);
        //         command.Parameters.AddWithValue("@ConsumoLuz", ConsumoLuz);
        //         command.Parameters.AddWithValue("@ConsumoAgua", ConsumoAgua);
        //         command.Parameters.AddWithValue("@ConsumoGas", ConsumoGas);
        //         command.Parameters.AddWithValue("@Temperatura", Temperatura);
        //         command.Parameters.AddWithValue("@Fecha", fecha);

        //         command.ExecuteNonQuery();
        //     }
        //     connection.Close();
        // }
    }
    void Update()
    {
        time += Time.deltaTime;
        if(time > 3){
            updateValues();
            insertSensores();
            time = 0f;
        }
        DateTime fecha2 = globalVariables.getDia();
        if(fecha2 > fecha ) {
            updateValues();
            insertSensores();
        }
    }

    public void nearCoche(bool value)
    {
        cocheCerca = value;
    }
}
