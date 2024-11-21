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

    // Start is called before the first frame update
    void Start()
    {
        cambiarPantallaScript = GetComponent<NavigateToAfterTimeOrPress>();
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
            yield return new WaitForSeconds(0.5f);
            text.text = "-PRESS ANY BUTTON-";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void onStartGame(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(escenaACargar);
        //LoadingScene.instance.voidLoadScene(escenaACargar);

    }

    private void OnDisable()
    {
        start.action.Disable();

        start.action.performed -= onStartGame;
        start.action.canceled -= onStartGame;
    }
}
