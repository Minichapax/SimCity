using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {

            Farolaca padreObject = gameObject.transform.parent.GetComponent<Farolaca>();

            if (padreObject != null)
            {
                padreObject.nearPerson(true);
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {

            Farolaca padreObject = gameObject.transform.parent.GetComponent<Farolaca>();

            if (padreObject != null)
            {
                padreObject.nearPerson(false);
            }
            
        }
    }
}
