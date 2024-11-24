using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] Button back;
    [SerializeField] GameObject mainMenu;

    [SerializeField] AudioMixer audioMixer;

    CanvasGroup interactableCanvas;
    // Start is called before the first frame update

    private void OnEnable()
    {
        interactableCanvas = mainMenu.GetComponent<CanvasGroup>();
        back.onClick.AddListener(BackMainMenu);
    }

    void Start()
    {
       
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
        audioMixer.SetFloat("Master", volume);
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    private void OnDisable()
    {
        back.onClick.RemoveListener(BackMainMenu);
    }
}
