using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasOptions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Button resume;
    [SerializeField] Button howTo;
    [SerializeField] Button options;
    [SerializeField] Button mainMenu;


    private string escenaACambiar;

    [SerializeField] GameObject canvasOptions;
    [SerializeField] GameObject canvasHowTo;

    CanvasGroup interactableCanvas;

    AudioSource camSound;
    private void OnEnable()
    {
        resume.onClick.AddListener(ResumeGame);
        howTo.onClick.AddListener(HowToPlayClicked);
        options.onClick.AddListener(OptionsMenu);
        mainMenu.onClick.AddListener(BackToMainMenu);


    }

    private void Start()
    {
        interactableCanvas = GetComponent<CanvasGroup>();
        camSound = Camera.main.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HowToPlayClicked()
    {
        interactableCanvas.interactable = false;
        canvasHowTo.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        camSound.UnPause();
        gameObject.SetActive(false);
    }

    public void OptionsMenu()
    {
        interactableCanvas.interactable = false;
        canvasOptions.SetActive(true);
    }

    public void BackToMainMenu() 
    {
        escenaACambiar = "MainMenu";
        SceneManager.LoadSceneAsync(escenaACambiar);
    }

    private void OnDisable()
    {
        resume.onClick.RemoveListener(ResumeGame);
        howTo.onClick.RemoveListener(HowToPlayClicked);
        options.onClick.RemoveListener(OptionsMenu);
        mainMenu.onClick.RemoveListener(BackToMainMenu);


    }
}
