using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static globalVariables;
using System;

public class Light_Script : MonoBehaviour
{
    float x = 0;

    private globalVariables globalVariables;
    // Start is called before the first frame update
    void Start()
    {
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();

    }
    
    void Update () {
        x = (float)Math.Sin(((globalVariables.getHoraDelDia())/(229.18266f)))*360f;
        transform.rotation = Quaternion.Euler(x+180,0,0);
    }

}
