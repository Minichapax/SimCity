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
        rawImage = GetComponentInChildren<RawImage>();
        rawImage.texture = playIcon;
    }

    public void OnButtonClick()
    {
        if (isSelected)
        {
            rawImage.texture = pauseIcon;
            isSelected = true;
        }else
        {
            rawImage.texture = playIcon;
            isSelected = false;
        }
    }
}