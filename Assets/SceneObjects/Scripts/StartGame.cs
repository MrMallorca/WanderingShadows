using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartGame : MonoBehaviour
{
    private string escenaACargar = "MainMenu";
    [SerializeField] InputActionReference start;

    [SerializeField] TextMeshProUGUI text;

    NavigateToAfterTimeOrPress cambiarPantallaScript;

    float timeToBlink;

    [SerializeField] AudioSource camSound2;

    [SerializeField] AudioMixer audioMixer;
    float masterVSliderValue;
    float sFXSliderValue;
    float musicSliderValue;


    // Start is called before the first frame update
    void Start()
    {
        cambiarPantallaScript = GetComponent<NavigateToAfterTimeOrPress>();

        timeToBlink = 0.5f;

        StartCoroutine(BlinkText());

        masterVSliderValue = PlayerPrefs.GetFloat("Master", 0.5f);
        audioMixer.SetFloat("Master", Mathf.Log10(masterVSliderValue) * 20);

        sFXSliderValue = PlayerPrefs.GetFloat("SFX", 0.5f);
        audioMixer.SetFloat("SFX", Mathf.Log10(sFXSliderValue) * 20);

        musicSliderValue = PlayerPrefs.GetFloat("Music", 0.5f);
        audioMixer.SetFloat("Music", Mathf.Log10(musicSliderValue) * 20);


    }
    private void OnEnable()
    {
        start.action.Enable();

        start.action.performed += onStartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            text.text = " ";
            yield return new WaitForSeconds(timeToBlink);
            text.text = "-PRESS ANY BUTTON-";
            yield return new WaitForSeconds(timeToBlink);
        }
    }

    public void onStartGame(InputAction.CallbackContext ctx)
    {
       
        StartCoroutine(LaunchGame());
        
    }

    IEnumerator LaunchGame()
    {
        timeToBlink = 0.1f;
        camSound2.Play();


        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(escenaACargar);
       
    }

    private void OnDisable()
    {
        start.action.Disable();

        start.action.performed -= onStartGame;
    }
}
