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

    string escenaACambiar;

    [SerializeField] GameObject canvasPause;

    public List<ICharacterStatus> characters;

    public AudioClip[] audios;

    Camera camera;
    AudioSource audioCamera;

    bool isPlayingGameBeginAudio = false;


    private void Awake()
    {
        camera = Camera.main;

        audioCamera = camera.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        escenaACambiar = "Level1";

        characters = new List<ICharacterStatus>(FindObjectsOfType<MonoBehaviour>().
            OfType<ICharacterStatus>());

        StartCoroutine(PlayMusic(audios[0]));


    }

    // Update is called once per frame
    void Update()
    {
      
        foreach (var character in characters)
        {
            if (character.Hit)
            {
                vidas = Mathf.Clamp(vidas - 1, 0, healthSprites.Length - 1);
                healthBar.sprite = healthSprites[vidas];

                (character as PlayerScript).isHitted = false;

            }

            if (character.Dead)
            {
                (character as PlayerScript).isDead = true;
                StartCoroutine(ReloadScene());

            }
            //if(character.GameBegin && !isPlayingGameBeginAudio) 
            //{
            //    audioCamera.loop = true;
            //    StartCoroutine(PlayMusic(audios[1]));

            //}
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

    IEnumerator PlayMusic(AudioClip audioClip)
    {
        //audioCamera.Stop();
        //audioCamera.clip = audioClip;
        audioCamera.PlayOneShot(audioClip);
        yield return null;
    }
   
}
