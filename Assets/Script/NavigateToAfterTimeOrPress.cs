using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class NavigateToAfterTimeOrPress : MonoBehaviour
{
    private string escenaACargar = "SplashGame";
    private float tiempoParaCambiar = 5f;
    [SerializeField] InputActionReference skip;
    bool cargarEscena = false;

    // Start is called before the first frame update
    void Awake()
    {
        NavigateToNextScreen(escenaACargar);
    }

    private void OnEnable()
    {
        skip.action.Enable();

        skip.action.performed += OnSkip;
        skip.action.canceled += OnSkip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavigateToNextScreen(string escena)
    {
        cargarEscena = true;

        if (cargarEscena)
        {
            Invoke("CargarEscena", tiempoParaCambiar);
        }
    }

    private void CargarEscena()
    {
        SceneManager.LoadScene(escenaACargar);
    }
    void OnSkip(InputAction.CallbackContext ctx)
    {
        NavigateToNextScreen(escenaACargar);

    }

   

    private void OnDisable()
    {
        skip.action.Disable();

        skip.action.performed -= OnSkip;
        skip.action.canceled -= OnSkip;
    }
}
