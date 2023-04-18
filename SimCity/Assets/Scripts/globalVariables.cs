using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class globalVariables : MonoBehaviour
{

    private float horaDelDia; //   30º Dia 145º Noche 30º
    private int velocidadDia = 15;
    private int dia = 0;
    // Start is called before the first frame update
    void Start()
    {
        horaDelDia = 30;
    }

    // Update is called once per frame
    void Update()
    {
        horaDelDia += Time.deltaTime * velocidadDia;
        if (horaDelDia >= 360){ 
            horaDelDia = 0;
            dia += 1;
        }
    }

    public DateTime getDia(){
        return DateTime.Today.AddDays(dia);
    }

    public string getHoraDelDiaHHMMSS(){

        int hora = Mathf.FloorToInt(horaDelDia/15f);
        int minutos = Mathf.FloorToInt((horaDelDia-hora*15f)/0.25f);
        int segundos = Mathf.FloorToInt((horaDelDia-hora*15f-minutos*0.25f)/0.00416F);

        string result = string.Format("{0}:{1}:{2}", hora.ToString("D2"), minutos.ToString("D2"), segundos.ToString("D2"));

        return result;
    }

    public float getHoraDelDia(){ return horaDelDia; }
    
    public void pararTiempo(){
        if(velocidadDia != 0) velocidadDia = 0;
        else velocidadDia = 30;
    }

    public void toggleNocheDia(){
    
        if (horaDelDia >= 175 || horaDelDia < 20){   // Si es de noche hacer de día       
            horaDelDia = 20;
        }else{ // Si es de día hacer de noche
            horaDelDia = 175;
        }
    
    }

}
