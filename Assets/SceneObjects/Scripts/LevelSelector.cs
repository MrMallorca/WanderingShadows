using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LevelSelector : MonoBehaviour
{

    [SerializeField] Button ninjaWayBtn;
    [SerializeField] Button samuraiWayBtn;
    [SerializeField] Button geishaWayBtn;
    [SerializeField] Button back;


    [SerializeField] Animator crossfadeAnim;
    private float transitionTime = 1f;
    private string escenaACambiar;


    // Start is called before the first frame update

    private void Awake()
    {
        ninjaWayBtn.onClick.AddListener(LoadNinjaLevel);
        samuraiWayBtn.onClick.AddListener(LoadSamuraiLevel);
        geishaWayBtn.onClick.AddListener(LoadGeishaLevel);
        back.onClick.AddListener(BackToMainMenu);

    }
    void Start()
    {

        crossfadeAnim.Play("Crossfade_End");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNinjaLevel()
    {

        escenaACambiar = "Level1";
        StartCoroutine(LoadLevel(escenaACambiar));

    }

    public void LoadSamuraiLevel()
    {
        escenaACambiar = "Level2";
        StartCoroutine(LoadLevel(escenaACambiar));
    }
    public void LoadGeishaLevel()
    {
        escenaACambiar = "Level1";
        StartCoroutine(LoadLevel(escenaACambiar));
    }

    public void BackToMainMenu()
    {
        escenaACambiar = "MainMenu";
        StartCoroutine(LoadLevel(escenaACambiar));
    }

    IEnumerator LoadLevel(string levelName)
    {
        Debug.Log("entro");

        crossfadeAnim.SetTrigger("Start");


        yield return new WaitForSeconds(transitionTime);

        Debug.Log("paso");
        

        SceneManager.LoadSceneAsync(levelName);



    }

   

    void OnDisable()
    {
        ninjaWayBtn.onClick.RemoveListener(LoadNinjaLevel);
        samuraiWayBtn.onClick.RemoveListener(LoadSamuraiLevel);
        geishaWayBtn.onClick.RemoveListener(LoadGeishaLevel);
        back.onClick.RemoveListener(BackToMainMenu);


    }
}
