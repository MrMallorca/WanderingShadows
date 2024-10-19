using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene(0);

    }
}
