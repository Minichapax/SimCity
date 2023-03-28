using UnityEngine;
using UnityEngine.UI;

public class botonPararScript : MonoBehaviour
{
    public RawImage buttonIcon;
    public Texture playIcon;
    public Texture pauseIcon;
    private bool isPlaying = true;

    void Start()
    {
        // Asignamos el icono de play al inicio
        buttonIcon.texture = playIcon;

    }

    public void OnClick()
    {
        // Cambiamos el icono y el estado de reproducción
        if (isPlaying)
        {
            buttonIcon.texture = pauseIcon;
            isPlaying = false;
            // Aquí iría el código para pausar la reproducción
        }
        else
        {
            buttonIcon.texture = playIcon;
            isPlaying = true;
            // Aquí iría el código para reanudar la reproducción
        }
    }
}