using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClimaScript : MonoBehaviour
{
    public Texture[] textures;
    public Texture luna;

    private globalVariables globalVariables;
    private RawImage rawImage;
    private int randomIndex;
    private bool dia = false;
    private bool noche = true;
    // Start is called before the first frame update
    void Start()
    {
        globalVariables =  GameObject.Find("Plane").GetComponent<globalVariables>();
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(noche && rawImage.texture != luna){
            rawImage.texture = luna;
        }

        if(noche && globalVariables.getHoraDelDia() > 30600){
            noche=false;
            dia=true;
            randomIndex = Random.Range(0, textures.Length);
            rawImage.texture = textures[randomIndex];
        }

        if(dia && (globalVariables.getHoraDelDia() > 73800 || globalVariables.getHoraDelDia() < 3600)){
            noche=true;
            dia=false;
            rawImage.texture = luna;
        }
    }
}
