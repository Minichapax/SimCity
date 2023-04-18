using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class farolasScript : MonoBehaviour
{

    private globalVariables globalVariables;
    public bool farolaOn;
    public bool automaticFaroleishions; 
    private string horaDelDia;

    // Start is called before the first frame update
    void Start()
    {
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        farolaOn = false;
        automaticFaroleishions = true;
    }

    // Update is called once per frame
    void Update()
    {
        horaDelDia = globalVariables.getHoraDelDiaHHMMSS().Substring(0,2);
        int hora = int.Parse(horaDelDia);
        if(automaticFaroleishions){
            if (hora<8 || hora>=19){   // Si es de noche encender farolas    
                farolaOn = true;
            }else{ // Si es de día apagar farolas
                farolaOn = false;
            }
        }
    }

    public void togggleFarolas(){
        if ( automaticFaroleishions ){
            automaticFaroleishions = false;
        } else automaticFaroleishions = true;
        
        if (farolaOn) farolaOn = false;
        else farolaOn = true;
    }
}
