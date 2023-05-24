using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class horaScript : MonoBehaviour
{
    public TextMeshProUGUI textObjecthora;
    public TextMeshProUGUI textObjectDia;
    public TextMeshProUGUI textObjectDiaSemana;
    private globalVariables globalVariables;
    void Start()
    {
        // Invocamos el método "ChangeText" cada 10 segundos, empezando después de 1 segundo
        InvokeRepeating("ChangeText", 0.05f, 0.001f);
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();

    }

    void ChangeText()
    {
        textObjecthora.text = globalVariables.getHoraDelDiaHHMMSS();
        textObjectDia.text = globalVariables.getDia().ToString("dd/MM/yyyy");
        textObjectDiaSemana.text = globalVariables.getDiaSemana();
    }
}