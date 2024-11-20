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

    bool isPaused = false;



    private void Awake()
    {
        camera = Camera.main;

        audioCamera = camera.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        escenaACambiar = SceneManager.GetActiveScene().name;

        if(escenaACambiar == "Level1")
        {
            StartCoroutine(PlayMusic(audios[0], 0f, false));
            StartCoroutine(PlayMusic(audios[1], audios[0].length + 1, true));

        }
        else
        {
            StartCoroutine(PlayMusic(audios[1], 0f, true));

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseMenu();
            }

            isPaused = !isPaused;


        }
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
        audioCamera.Pause();
        Time.timeScale = 0f;
        canvasPause.SetActive(true);
    }
    public void ResumeGame()
    {
        audioCamera.Play();
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
    }


    IEnumerator PlayMusic(AudioClip audioClip, float time, bool loop)
    {
        yield return new WaitForSeconds(time);
        audioCamera.clip = audioClip;
        if(loop)
        {
            audioCamera.Play();
            audioCamera.loop = loop;
        }
        else
        {
            audioCamera.PlayOneShot(audioClip);
        }

    }

}
