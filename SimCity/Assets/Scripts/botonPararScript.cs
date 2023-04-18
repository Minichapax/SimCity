using UnityEngine;
using UnityEngine.UI;

public class botonPararScript : MonoBehaviour
{
    
    public Texture2D pauseIcon;
    public Texture2D playIcon;

    private RawImage rawImage;
    private bool isSelected = false;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.texture = pauseIcon;
    }

    void Update(){
        if(isSelected){
            rawImage.texture = pauseIcon;
        }else{
            rawImage.texture = playIcon;
            }

    }

    public void changeBoton()
    {
        isSelected = !isSelected;
    }
}