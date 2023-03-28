using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tablitaEdificiosScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    private bool isHola = true;

    void Start()
    {
        // Invocamos el método "ChangeText" cada 10 segundos, empezando después de 1 segundo
        InvokeRepeating("ChangeText", 1f, 2f);
    }

    void ChangeText()
    {
        // Cambiamos el texto del objeto "Text" cada 10 segundos
        if (isHola)
        {
            textObject.text = "Adiós";
            isHola = false;
        }
        else
        {
            textObject.text = "Hola";
            isHola = true;
        }
    }
}