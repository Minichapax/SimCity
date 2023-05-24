using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelCanvasEmergencias : MonoBehaviour
{
    public TextMeshProUGUI myTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); 
    }

    // Update is called once per frame
    public void UpdateTexts(string text){
        myTextMesh.text = text;
    }

    public void setActive(bool value){
        gameObject.SetActive(value);
    }
}
