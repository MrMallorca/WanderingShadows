using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static int vidas = 2;

    public Sprite[] healthSprites;
    public Image healthBar;

    [SerializeField] GameObject canvasPause;

    string escenaACambiar;
    public AudioClip[] audios;

    Camera camera;
    AudioSource audioCamera;



    private void Awake()
    {
        camera = Camera.main;

        audioCamera = camera.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        escenaACambiar = SceneManager.GetActiveScene().name;

        if(SceneManager.GetActiveScene().name == "Level1")
        {
            PlayMusic(audios[0]);
        }


    }

    // Update is called once per frame
    void Update()
    {
      
            if (PlayerScript.currentPlayer.Hit)
            {
                vidas = Mathf.Clamp(vidas - 1, 0, healthSprites.Length - 1);

                PlayerScript.currentPlayer.isHitted = false;

            }

            if (PlayerScript.currentPlayer.Dead)
            {
                StartCoroutine(ReloadScene());

            }
        healthBar.sprite = healthSprites[vidas];


    }



    public IEnumerator ReloadScene()
    {
        if(vidas == 0) 
        {
            yield return new WaitForSeconds(3.29f);
            vidas = 2;
            SceneManager.LoadScene(escenaACambiar);

        }
        else
        {
            SceneManager.LoadScene(escenaACambiar);
        }

    }
    public void PauseMenu()
    {
        Time.timeScale = 0f;
        canvasPause.SetActive(true);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        //audioCamera.Stop();
        //audioCamera.clip = audioClip;
        audioCamera.PlayOneShot(audioClip);
    }
   
}
