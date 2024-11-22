using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private string escenaACargar = "MainMenu";
    [SerializeField] InputActionReference start;

    [SerializeField] TextMeshProUGUI text;

    NavigateToAfterTimeOrPress cambiarPantallaScript;

    float timeToBlink;

    AudioSource camSound;
    [SerializeField] AudioClip[] clips;



    // Start is called before the first frame update
    void Start()
    {
        cambiarPantallaScript = GetComponent<NavigateToAfterTimeOrPress>();

        camSound = Camera.main.GetComponent<AudioSource>();

        camSound.clip = clips[0];
        camSound.Play();
        timeToBlink = 0.5f;

        StartCoroutine(BlinkText());


    }
    private void OnEnable()
    {
        start.action.Enable();

        start.action.performed += onStartGame;
        start.action.canceled += onStartGame;
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
        ctx.action.Disable();
        StartCoroutine(LaunchGame());

    }

    IEnumerator LaunchGame()
    {
        timeToBlink = 0.1f;
        camSound.Pause();
        camSound.clip = clips[1];
        camSound.Play();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(escenaACargar);
       
    }

    private void OnDisable()
    {
        start.action.Disable();

        start.action.performed -= onStartGame;
        start.action.canceled -= onStartGame;
    }
}
