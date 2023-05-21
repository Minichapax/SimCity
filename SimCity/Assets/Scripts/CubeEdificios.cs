using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeEdificios : MonoBehaviour
{
    private Edificio padreObject;
    void Awake(){
        padreObject = gameObject.transform.parent.GetComponent<Edificio>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Coche"))
        {
            if (padreObject != null)
            {
                padreObject.nearCoche(true);
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Coche"))
        {
            if (padreObject != null)
            {
                padreObject.nearCoche(false);
            }
            
        }
    }
}
