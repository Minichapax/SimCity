using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static farolasScript;
public class Farolaca : MonoBehaviour
{
    private bool farolaOn; 
    private bool nearPersonBool = false;
    public Light miLuz;
    // Start is called before the first frame update
    void Start()
    {
        miLuz = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        farolaOn = transform.parent.GetComponent<farolasScript>().farolaOn;
        if(farolaOn){ 
            miLuz.intensity = 1;
            if(nearPersonBool){
                miLuz.intensity = 2;
            }
        }
        else miLuz.intensity = 0;
    }

    public void nearPerson(bool value){
        nearPersonBool = value;
    }
}
