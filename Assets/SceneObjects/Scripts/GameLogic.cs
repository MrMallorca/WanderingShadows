using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class GameLogic : MonoBehaviour
{
    public static int vidas = 2;

    public Sprite[] healthSprites;
    public Image healthBar;

    [SerializeField] GameObject canvasPause;
    [SerializeField] GameObject canvasHowTo;
    [SerializeField] GameObject canvasOptions;

    string escenaACambiar;
    public AudioClip[] audios;

    Camera camera;
    AudioSource audioCamera;

    bool isPaused = false;

    [SerializeField] AudioMixer audioMixer;
    float masterVSliderValue;
    float sFXSliderValue;
    float musicSliderValue;

    private void Awake()
    {
        camera = Camera.main;

        audioCamera = camera.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        escenaACambiar = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;

        masterVSliderValue = PlayerPrefs.GetFloat("Master", 0.5f);
        audioMixer.SetFloat("Master", Mathf.Log10(masterVSliderValue) * 20);

        sFXSliderValue = PlayerPrefs.GetFloat("SFX", 0.5f);
        audioMixer.SetFloat("SFX", Mathf.Log10(sFXSliderValue) * 20);

        musicSliderValue = PlayerPrefs.GetFloat("Music", 0.5f);
        audioMixer.SetFloat("Music", Mathf.Log10(musicSliderValue) * 20);

        if (escenaACambiar == "Level1")
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

        if (Keyboard.current.escapeKey.wasPressedThisFrame 
            && !canvasHowTo.activeSelf 
            && !canvasOptions.activeSelf)
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
            vidas = 2;
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
        audioCamera.UnPause();
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
