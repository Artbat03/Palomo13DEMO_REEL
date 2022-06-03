using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Controller : MonoBehaviour
{
    // Variables
    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider brightnessSlider;
    public Dropdown resolutionDropdown;

    [Header("Panel")]
    [Space(25)]
    public Image brightnessPanel;

    [Header("Images")]
    [Space(25)]
    [SerializeField] private Image muteImage;
    [SerializeField] private Image unmuteImage;

    private float volumeSliderValue;
    private float brightnessSliderValue;
    Resolution[] screenResolutions;

    void Start()
    {
        // Crear valores predeterminados
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 0.5f);

        if (volumeSliderValue == 0)
        {
            muteImage.enabled = true;
            unmuteImage.enabled = false;
        }

        else
        {
            muteImage.enabled = false;
            unmuteImage.enabled = true;
        }
  
        // Volumen del juego de 0 a 1 ( muted or max volume)
        AudioListener.volume = volumeSlider.value;

        // Revisar muteado
        AmIMuted();

        // Color del panel del alfa igual al del valor del alfa (slider value)
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSliderValue);

        // Llamamos al m�todo que se encarga de revisar las resoluciones
        CheckScreenResolution();
    }

    // M�todo para cambiar el valor del slider del volumen
    public void ChangeVolumeSlider(float value)
    {
        volumeSliderValue = value;

        // Guardado del valor al mover el slider para cuando se cierren las options o el juego
        PlayerPrefs.SetFloat("volume", volumeSliderValue);
        AudioListener.volume = volumeSlider.value;

        // Revisar muteado
        AmIMuted();
    }

    // M�todo para cambiar el valor del slider del brillo
    public void ChangeBrightnessSlider(float value)
    {
        brightnessSliderValue = 1 - value;

        // Guardado del valor al mover el slider para cuando se cierren las options o el juego
        PlayerPrefs.SetFloat("brightness", brightnessSliderValue);

        // Cambiamos el color del panel segun el valor del slider
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightnessSliderValue);
    }

    private void AmIMuted()
    {
        // Si est� muteado, mostrar imagen de muteado
        if (volumeSliderValue == 0)
        {
            muteImage.enabled = true;
            unmuteImage.enabled = false;
        }

        // Si no est� muteado, no mostrar imagen de muteado
        else
        {
            muteImage.enabled = false;
            unmuteImage.enabled = true;
        }
    }

    // 
    private void CheckScreenResolution()
    {
        // Guardado de todas las resoluciones de cada ordenador
        screenResolutions = Screen.resolutions;

        // Borrar las opciones predeterminadas del dropdown
        resolutionDropdown.ClearOptions();

        // Lista de strings para guardar el tama�o de la resoluci�n
        List<string> options = new List<string>();

        // Variable para iniciar de 0
        int actualResolution = 0;

        for (int i = 0; i < screenResolutions.Length; i++)
        {
            // Mostrar las resoluciones en la barra de opciones del dropdown (Ex: 1920x1080)
            string option = screenResolutions[i].width + " x " + screenResolutions[i].height;
            options.Add(option);

            // Revisado de la opci�n guardada para guardar la resoluci�n actual de la pantalla
            if (screenResolutions[i].width == Screen.currentResolution.width && screenResolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        // Agregado de opciones guardadas en la lista
        resolutionDropdown.AddOptions(options);

        // Detecci�n de la resoluci�n en la que estamos
        resolutionDropdown.value = actualResolution;

        // Actualizado de la lista
        resolutionDropdown.RefreshShownValue();

        // Valor predeterminado para el primer inicio del juego
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", 0);
    }

    // M�todo para cambiar la resoluci�n en el dropdown
    public void ChangeScreenResolution(int resolutionIndex)
    {
        // Cambiado del valor y guardado de este mismo una vez cerrado el juego y mostrado en pantalla 
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);

        // Creado moment�neo de un valor de resoluci�n
        Resolution resolution = screenResolutions[resolutionDropdown.value];

    }
}
