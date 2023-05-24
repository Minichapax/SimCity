using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBasuras : MonoBehaviour
{
    public GameObject contenedores;
    public PanelCanvasBasuras panel;
    public GameObject rutaCamion;
    public GameObject camion;
    public float velocidadCoche;

    private Node[] nodosRuta;
    private float[] containers = new float[6];
    private float[] containersUltimoDia = new float[6];
    private float media;
    private globalVariables globalVariables;
    private bool open;
    private bool pasoCamion = false;
    private bool parada = false;
    private bool camionPasando = false;
    private string text="";
    private float tiempo;
    private float distancia;
    private float tiempoParada=0;
    static Vector3 posicionActual;
    static Vector3 posicionAnterior;
    private int num_nodo;
    // Start is called before the first frame update
    void Start()
    {
        open=false;
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        nodosRuta=rutaCamion.GetComponentsInChildren<Node>();
        for (int i = 0; i < containers.Length; i++){
            containers[i]=80;
        }
        calcularMedia();
    }

    private void calcularMedia(){
        float sum = 0f;
        for (int i = 0; i < containers.Length; i++){
            sum += containers[i];
        }
        media = sum / containers.Length;

        text="Nivel de ocupación de basuras: "+media.ToString("F2")+"%";
        panel.UpdateTexts(text);
    }

    private void CheckNode(){
        tiempo=0;
        posicionAnterior=nodosRuta[num_nodo].transform.position;
        num_nodo++;
        if(num_nodo == nodosRuta.Length){ 
            num_nodo = 0;
            camionPasando = false;
            camion.SetActive(false);
        }
        else if(num_nodo == 5){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[5]=containers[5];
            containers[5]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else if(num_nodo == 8){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[4]=containers[4];
            containers[4]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else if(num_nodo == 12){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[3]=containers[3];
            containers[3]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else if(num_nodo == 25){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[2]=containers[2];
            containers[2]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else if(num_nodo == 33){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[0]=containers[0];
            containers[0]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else if(num_nodo == 34){
            tiempoParada=0;
            parada = true;
            containersUltimoDia[1]=containers[1];
            containers[1]=0;
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
            calcularMedia();
        }
        else{
            posicionActual=nodosRuta[num_nodo].transform.position;
            camion.transform.LookAt(posicionActual);
            camion.transform.Rotate(0, -90, 0);
            distancia = Vector3.Distance(posicionAnterior,posicionActual);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){

        if(globalVariables.getHoraDelDia() > 21600 && globalVariables.getHoraDelDia() < 84600){
            for (int i = 0; i < containers.Length; i++){
                if (containers[i] < 100){
                    containers[i] += Random.Range(0.00001f, 0.00005f)*globalVariables.velocidadDia;
                }
            }
            calcularMedia();
        }

        if(globalVariables.getHoraDelDia() > 84600){
            for (int i = 0; i < containers.Length; i++){
                if (containers[i] < 94){
                    containers[i] += Random.Range(0, 5);
                }
            }
            calcularMedia();
        }
    
        if(globalVariables.getHoraDelDia() > 3600 && globalVariables.getHoraDelDia() < 5400 && !pasoCamion){
            pasoCamion=true;
            camionPasando=true;
            camion.SetActive(true);
            num_nodo=0;
            camion.transform.position=nodosRuta[0].transform.position;
            CheckNode();
        }

        if(camionPasando){
            if(parada){
                tiempoParada += Time.deltaTime*globalVariables.velocidadDia;
                if(tiempoParada > 15.0f) parada = false;
            }
            else{
                tiempo += Time.deltaTime * globalVariables.velocidadDia * velocidadCoche / (distancia*15);
                if(camion.transform.position != posicionActual){
                    camion.transform.position = Vector3.Lerp(posicionAnterior, posicionActual, tiempo);
                }
                else{
                    CheckNode();
                }
            }
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
