using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class globalVariables : MonoBehaviour
{

    private float horaDelDia; //   30º Dia 145º Noche 30º
    public int velocidadDia = 10000;
    private int dia = 0;
    // Start is called before the first frame update
    void Start()
    {
        horaDelDia = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horaDelDia += Time.deltaTime * velocidadDia;
        if (horaDelDia >= 86400){ 
            horaDelDia = 0;
            dia += 1;
        }
    }

    public DateTime getDia(){
        return DateTime.Today.AddDays(dia);
    }

    public string getHoraDelDiaHHMMSS(){

        int hora = Mathf.FloorToInt(horaDelDia/3600);
        int minutos = Mathf.FloorToInt((horaDelDia-hora*3600)/60);
        int segundos = Mathf.FloorToInt((horaDelDia-hora*3600-minutos*60));

        string result = string.Format("{0}:{1}:{2}", hora.ToString("D2"), minutos.ToString("D2"), segundos.ToString("D2"));

        return result;
    }

    public float getHoraDelDia(){ return horaDelDia; }
    
    public void pararTiempo(){
        if(velocidadDia != 0) velocidadDia = 0;
        else velocidadDia = 30;
    }

    public void toggleNocheDia(){
    
        if (horaDelDia >= 68400 || horaDelDia < 32400){   // Si es de noche hacer de día       
            horaDelDia = 32400;
        }else{ // Si es de día hacer de noche
            horaDelDia = 68400;
        }
    
    }

}
