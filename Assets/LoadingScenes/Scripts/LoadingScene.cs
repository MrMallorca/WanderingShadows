using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] string firstSceneToLoad;
    [SerializeField] CanvasGroup fader;
    Scene currentScene;

    static public LoadingScene instance;
    static bool wasLoadedOnPlayModeStateChange;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]

    static void ListenToApplicationModeChange()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    private static void OnPlayModeStateChange(PlayModeStateChange change)
    {
        wasLoadedOnPlayModeStateChange = false;

        if (change == PlayModeStateChange.EnteredPlayMode)
        {
            if (SceneManager.GetSceneAt(0).name != "LoadingScene")
            {
                SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
                wasLoadedOnPlayModeStateChange = true;
                Scene loadingScene = SceneManager.GetSceneAt(SceneManager.loadedSceneCount - 1);
            }
        }
    }
#endif
    private void Awake()
    {
        instance = this;

    }


    private void Start()
    {
        if (!wasLoadedOnPlayModeStateChange)
        {
            LoadScene(firstSceneToLoad);
        }
        else
        {
            currentScene = SceneManager.GetSceneAt(0);
            fader.DOFade(0f, 0f);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
   public IEnumerator LoadSceneCoroutine(string sceneName)
   {
        //Fade
        { 
            Tween fadeTween = fader.DOFade(1f, 1f);
                do
                {
                    yield return new();
                }
                while (fadeTween.IsPlaying());
        }

        //Descargar la escena actual
        if(currentScene.isLoaded)
        {
           AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);
            do
            {
                yield return new();
            }
            while (!unloadOperation.isDone);


        }

        //Cargar la escena
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            do
            {
                yield return new();
            }
            while (!loadOperation.isDone);  

            currentScene = SceneManager.GetSceneAt(SceneManager.loadedSceneCount - 1);
            SceneManager.SetActiveScene(currentScene);
        }


        //Fade
        {
            {
                Tween fadeTween = fader.DOFade(0f, 1f);
                do
                {
                    yield return new();
                }
                while (fadeTween.IsPlaying());
            }
        }
    }
#if UNITY_EDITOR
    [MenuItem("LoadingScene/Debug/Change to OutdoorsScene")]

    static public void DebugChangeToOutdoorsScene()
    {
        LoadingScene.instance.LoadScene("MainMenu");
    }
#endif 
}


