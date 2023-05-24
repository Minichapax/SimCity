using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coche : MonoBehaviour
{
    public string Matricula;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string getMatricula(){
        return Matricula;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive(bool value){
        gameObject.SetActive(value);
    }

    public void lookAt(Vector3 posicion){
        transform.LookAt(posicion);
    }
}
