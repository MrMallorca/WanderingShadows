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

    [SerializeField] AudioSource camSound2;


    // Start is called before the first frame update
    void Start()
    {
        cambiarPantallaScript = GetComponent<NavigateToAfterTimeOrPress>();

        timeToBlink = 0.5f;

        StartCoroutine(BlinkText());


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
