using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private string escenaACargar = "Level1";
    [SerializeField] InputActionReference start;
    bool cargarEscena = false;

    [SerializeField] TextMeshProUGUI text;

    NavigateToAfterTimeOrPress cambiarPantallaScript;

    // Start is called before the first frame update
    void Start()
    {
        cambiarPantallaScript = GetComponent<NavigateToAfterTimeOrPress>();
        StartCoroutine(BlinkText());
        Debug.Log(cambiarPantallaScript);
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
            text.text = "-PRESS START-";
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void NavigateToNextScreen(string escena)
    {
        cargarEscena = true;

        if (cargarEscena)
        {
            
             SceneManager.LoadScene(escenaACargar);
            
        }
    }
    void onStartGame(InputAction.CallbackContext ctx)
    {
        NavigateToNextScreen(escenaACargar);

    }

    private void OnDisable()
    {
        start.action.Disable();

        start.action.performed -= onStartGame;
        start.action.canceled -= onStartGame;
    }
}
