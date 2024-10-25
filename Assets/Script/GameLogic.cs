using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int vidas = 2;

    string escenaACambiar;

    [SerializeField] GameObject canvasPause;
    // Start is called before the first frame update
    void Start()
    {
        escenaACambiar = "Level1";
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.isDead)
        {
            StartCoroutine(ReloadScene());
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3.29f);
        SceneManager.LoadScene(escenaACambiar);

    }

    public void PauseMenu()
    {
        Time.timeScale = 0f;
        canvasPause.SetActive(true);
    }
   
}
