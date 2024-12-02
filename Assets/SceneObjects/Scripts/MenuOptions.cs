using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] GameObject mainMenu;

    [SerializeField] AudioMixer audioMixer;

    CanvasGroup interactableCanvas;

    [SerializeField] Slider masterVSlider;
     float masterVSliderValue;

    [SerializeField] Slider sFXSlider;
    float sFXSliderValue;

    [SerializeField] Slider musicSlider;
    float musicSliderValue;

    [SerializeField] Slider brightnessSlider;


    [SerializeField] Toggle fullScreen;

    [SerializeField] TMP_Dropdown resolutionsDropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update

    private void OnEnable()
    {
        interactableCanvas = mainMenu.GetComponent<CanvasGroup>();
        back.onClick.AddListener(BackMainMenu);

    }

    void Start()
    {
        masterVSliderValue = PlayerPrefs.GetFloat("Master", 0.5f);
        masterVSlider.value = masterVSliderValue;
        audioMixer.SetFloat("Master", Mathf.Log10(masterVSliderValue) * 20);

        sFXSliderValue = PlayerPrefs.GetFloat("SFX", 0.5f);
        sFXSlider.value = sFXSliderValue;
        audioMixer.SetFloat("SFX", Mathf.Log10(sFXSliderValue) * 20);

        musicSliderValue = PlayerPrefs.GetFloat("Music", 0.5f);
        musicSlider.value = musicSliderValue;
        audioMixer.SetFloat("Music", Mathf.Log10(musicSliderValue) * 20);


        if(Screen.fullScreen)
        {
            fullScreen.isOn = true;
        }
        else
        {
            fullScreen.isOn = false;
        }

        RevisarResoluciones();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackMainMenu()
    {
        interactableCanvas.interactable = true;
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float volume)
    {
       
        masterVSliderValue = volume;
        PlayerPrefs.SetFloat("Master", masterVSliderValue);
        audioMixer.SetFloat("Master", Mathf.Log10(masterVSliderValue) * 20);
    }
    public void SetSfxVolume(float volume)
    {
        sFXSliderValue = volume;
        PlayerPrefs.SetFloat("SFX", sFXSliderValue);
        audioMixer.SetFloat("SFX", Mathf.Log10(sFXSliderValue) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        musicSliderValue = volume;
        PlayerPrefs.SetFloat("Music", musicSliderValue);
        audioMixer.SetFloat("Music", Mathf.Log10(musicSliderValue) * 20);
    }

    public void SetBrightness(float brightness)
    {
      
        
    }

    public void ActivateFullScreen(bool isFullScreened)
    {
        Screen.fullScreen = isFullScreened;
    }

    public void RevisarResoluciones()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string opcion = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolutionsDropdown.AddOptions(opciones);
        resolutionsDropdown.value = resolucionActual;
        resolutionsDropdown.RefreshShownValue();

        resolutionsDropdown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void ChangeResolution(int indexResolution)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolutionsDropdown.value);


        Resolution resolucion = resolutions[indexResolution];
        Screen.SetResolution(resolucion.width, resolucion.height,Screen.fullScreen);
    }
   
    private void OnDisable()
    {
        back.onClick.RemoveListener(BackMainMenu);
    }
}
