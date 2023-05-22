using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static globalVariables;
using System;

public class Light_Script : MonoBehaviour
{
    float x = 0;

    private globalVariables globalVariables;
    private bool emergencia=false;
    // Start is called before the first frame update
    void Start()
    {
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();

    }

    public void ToggleEmergencia(bool value){
        emergencia=value;
    }
    
    void Update () {
        x = (float)Math.Sin(((globalVariables.getHoraDelDia()*360/86400)/(229.18266f)))*360f;
        transform.rotation = Quaternion.Euler(x+180,0,0);

        if(emergencia){
            transform.rotation = Quaternion.Euler(20,0,0);
        }
    }

}
