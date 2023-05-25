using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using TMPro;

public class PanelParadas : MonoBehaviour
{
    bool open = false;
    public TextMeshProUGUI myTextMesh;

    static NpgsqlConnection GetConnection()
    {
        string connectionString = "Host=localhost;Username=alumnogreibd;Password=greibd2015;Database=campusInteligente";
         NpgsqlConnection connection = new NpgsqlConnection(connectionString);
         return connection;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); 
    }

    // Update is called once per frame
    public void UpdateTexts(string text){
        myTextMesh.text = text;
    }

    public void setActive(bool value){
        gameObject.SetActive(value);
    }


    public void botonParking(){
        if(open){
            setActive(false);
            open=false;
        }
        else{
            UpdateTexts(getTextoParadas());
            setActive(true);
            open=true;
        }
    }

    private string getTextoParadas(){
        string result = "Paradas:\n";
        using (NpgsqlConnection connection = GetConnection())
        {
            connection.Open();
            string query = "select * from parada_transporte order by fecha_y_hora_por_cada_parada desc limit 6;";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string parada = reader["parada"].ToString();
                        string fecha_y_hora_por_cada_parada = reader["fecha_y_hora_por_cada_parada"].ToString();
                        result += parada + " -> " +fecha_y_hora_por_cada_parada+ "\n";
                    }
                }
            }
        }

        return result;
    }
}
