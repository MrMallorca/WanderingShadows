using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;




public class MainMenu : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button options;
    [SerializeField] Button quit;

    [SerializeField] GameObject canvasOptions;

    private string escenaACambiar = "MenuSelector";
    // Start is called before the first frame update
    private void OnEnable()
    {
        play.onClick.AddListener(PlayGame);
        options.onClick.AddListener(OnOptionsButtonClicked);
        quit.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(escenaACambiar);
    }

    public void ExitGame()
    {
       Application.Quit();

    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;

    #endif
    }

    public void OnOptionsButtonClicked()
    {
        canvasOptions.SetActive(true);
        gameObject.SetActive(false);
    }
}
