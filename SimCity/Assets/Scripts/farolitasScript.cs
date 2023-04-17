using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static farolasScript;

public class farolitasScript : MonoBehaviour
{
    private bool farolaOn; 
    public Light miLuz;
    // Start is called before the first frame update
    void Start()
    {
        miLuz = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        farolaOn = transform.parent.transform.parent.GetComponent<farolasScript>().farolaOn;
        if(farolaOn) miLuz.intensity = 1;
        else miLuz.intensity = 0;
    }

    public void nearPerson(){
        if(true) miLuz.intensity = 2;
        //else miLuz.intensity = 1;
    }

}
