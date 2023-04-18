using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class horaScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    private globalVariables globalVariables;
    void Start()
    {
        // Invocamos el método "ChangeText" cada 10 segundos, empezando después de 1 segundo
        InvokeRepeating("ChangeText", 1f, 0.001f);
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();

    }

    void ChangeText()
    {
        textObject.text = globalVariables.getDia().ToString("dd/MM/yyyy") + " " + globalVariables.getHoraDelDiaHHMMSS();
    }
}