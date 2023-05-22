using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBasuras : MonoBehaviour
{
    public GameObject contenedores;
    public PanelCanvasBasuras panel;

    private float[] containers = new float[7];
    private float media;
    private globalVariables globalVariables;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open=false;
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        for (int i = 0; i < containers.Length; i++){
            if (containers[i] < 100){
                containers[i] += Random.Range(0.00001f, 0.00005f)*globalVariables.velocidadDia;
            }
        }

        float sum = 0f;
        for (int i = 0; i < containers.Length; i++){
            sum += containers[i];
        }
        media = sum / containers.Length;

        string text="Nivel de ocupación de basuras: "+media.ToString("F2")+"%";
        panel.UpdateTexts(text);
        Debug.Log(text);

        if(globalVariables.getHoraDelDia() < 21600){
            for (int i = 0; i < containers.Length; i++){
                containers[i] = 0;
            }
            media = 0;
            text="Nivel de ocupación de basuras: "+media+"%";
            panel.UpdateTexts(text);
        }
    
    }

    public void botonBasura(){
        if(open){
            panel.setActive(false);
            open=false;
        }
        else{
            panel.setActive(true);
            open=true;
        }
    }
}
