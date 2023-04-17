using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static globalVariables;

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
            x = globalVariables.getHoraDelDia();
            transform.rotation = Quaternion.Euler(x,0,0);
    }

}
