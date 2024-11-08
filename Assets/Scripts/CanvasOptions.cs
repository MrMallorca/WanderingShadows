using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasOptions : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Button resume;
    [SerializeField] Button options;
    [SerializeField] Button mainMenu;

    private void OnEnable()
    {
        resume.onClick.AddListener(ResumeGame);
        options.onClick.AddListener(OptionsMenu);
        mainMenu.onClick.AddListener(BackToMainMenu);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OptionsMenu()
    {

    }

    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

   
}
