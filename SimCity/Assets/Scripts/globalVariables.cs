using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVariables : MonoBehaviour
{

    private float horaDelDia; //   30º Dia 145º Noche 30º
    private int velocidadDia = 30;

    // Start is called before the first frame update
    void Start()
    {
        horaDelDia = 30;
    }

    // Update is called once per frame
    void Update()
    {
        horaDelDia += Time.deltaTime * velocidadDia;
        if (horaDelDia >= 360) horaDelDia = 0;
    }

    public float getHoraDelDia(){ return horaDelDia; }
    
    public void pararTiempo(){
        if(velocidadDia != 0) velocidadDia = 0;
        else velocidadDia = 30;
    }

    public void toggleNocheDia(){
    
        if (horaDelDia >= 145 || horaDelDia < 30){   // Si es de noche hacer de día       
            horaDelDia = 30;
        }else{ // Si es de día hacer de noche
            horaDelDia = 145;
        }
    
    }

}
