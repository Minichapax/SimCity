using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Light_Script : MonoBehaviour
{
    float x = 0;
    float velocidad = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    
    void Update () {
            x += Time.deltaTime * 30 * velocidad;
            transform.rotation = Quaternion.Euler(x,0,0);
            if(x > 360) x=0;
    }

    public void toggleNocheDia(){
        pararTiempo();
    
        if (x > 200){   // Si es de noche hacer de día       
            x = 100;
            transform.rotation = Quaternion.Euler(x,0,0);
        }else{ // Si es de día hacer de noche
            x = 200;
            transform.rotation = Quaternion.Euler(x,0,0);
        }
    
    }

    public void pararTiempo(){
        if(velocidad == 1) velocidad = 0;
        else velocidad = 1;
    }

}
