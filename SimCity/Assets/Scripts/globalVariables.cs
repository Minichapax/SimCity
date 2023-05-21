using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class globalVariables : MonoBehaviour
{

    private float horaDelDia; //   30º Dia 145º Noche 30º
    public int velocidadAnterior = 1;

    public int velocidadDia = 1;
    private int dia = 0;
    public Camera_Script cameraScript;
    private float tiempoMoveCamera;
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
        tiempoMoveCamera += Time.deltaTime;
        if(tiempoMoveCamera > 1) cameraScript.canMoveCamera = true;
    }

    public DateTime getDia(){
        return DateTime.Today.AddDays(dia).AddSeconds(horaDelDia);
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
        if(velocidadDia != 0){ 
            velocidadAnterior = velocidadDia;
            velocidadDia = 0; 
        }
        else velocidadDia = velocidadAnterior;
    }

    public void toggleNocheDia(){
    
        if (horaDelDia >= 68400 || horaDelDia < 32400){   // Si es de noche hacer de día       
            horaDelDia = 32400;
        }else{ // Si es de día hacer de noche
            horaDelDia = 68400;
        }
    
    }

    public void changeVelocidad(float velocidad){
        cameraScript.canMoveCamera = false;
        tiempoMoveCamera = 0.0f;
        switch(velocidad){
            case 1:
                velocidadDia = 1;
            break;
            case 2:
                velocidadDia = 2;
            break;
            case 3:
                velocidadDia = 4;
            break;
            case 4:
                velocidadDia = 60;
            break;
            case 5:
                velocidadDia = 300;
            break;
            case 6:
                velocidadDia = 1800;
            break;
            case 7:
                velocidadDia = 3600;
            break;
            case 8:
                velocidadDia = 18000;
            break;
            case 9:
                velocidadDia = 43200;
            break;
            case 10:
                velocidadDia = 86400;
            break;
        }
    }
}
