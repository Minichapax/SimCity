using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperaturaScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;

    private globalVariables globalVariables;
    private float Temperatura;
    private float tiempoHora=0;
    void Start()
    {
        // Invocamos el método "ChangeText" cada 10 segundos, empezando después de 1 segundo
        //InvokeRepeating("ChangeText", 0.5f, 1.5f);
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();

    }

    void Update(){
        tiempoHora += Time.deltaTime * globalVariables.velocidadDia;
        if(tiempoHora > 3600){
            tiempoHora=0;
            Temperatura  = UnityEngine.Random.Range(15f, 19f);
            if(globalVariables.getHoraDelDia()  > 82800 || globalVariables.getHoraDelDia() < 23400){
                Temperatura  = UnityEngine.Random.Range(10f, 14f);
            }
            textObject.text = Temperatura.ToString("F1")+ " ºC";
        }
    }

    void ChangeText()
    {
        Temperatura  = UnityEngine.Random.Range(15f, 19f);
        if(globalVariables.getHoraDelDia()  >= 82800 || globalVariables.getHoraDelDia() < 23400){
            Temperatura  = UnityEngine.Random.Range(10f, 14f);
        }
        textObject.text = Temperatura.ToString("F1")+ " ºC";
    }
}
