using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] CanvasGroup fader;
    Scene currentScene;

    public static LoadingScene instance;

    private void Awake()
    {
        instance = this;

    }

    public void voidLoadScene(string sceneName)
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

            currentScene = SceneManager.GetSceneAt(1);
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

    [MenuItem("LoadingScene/Debug/Change to OutdoorsScene")]

    static public void DebugChangeToOutdoorsScene()
    {
        LoadingScene.instance.voidLoadScene("MainMenu");
    }
}


