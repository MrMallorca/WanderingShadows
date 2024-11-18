using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeMechanics : MonoBehaviour
{

    [SerializeField] GameObject playerToInvoke;
    GameObject lastPlayer;

    Vector3 lastPlayerPosition;
    Quaternion lastPlayerRotation;

    [SerializeField] GameObject canvasVictory;


    // Start is called before the first frame update
    void Start()
    {
        lastPlayer = GameObject.FindWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        if (lastPlayer != null)
        {
            lastPlayerPosition = lastPlayer.transform.position;
            lastPlayerRotation = lastPlayer.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == lastPlayer)
        {
            if(gameObject.CompareTag("PuertaFinal"))
            {
                StartCoroutine(LoadNextLevel());
            }
            else
            {
                if(playerToInvoke != lastPlayer) 
                {
                    Destroy(lastPlayer);
                    Instantiate(playerToInvoke, lastPlayerPosition, lastPlayerRotation);
                }
               
               
            }
           
        }
    }

    IEnumerator LoadNextLevel()
    {
        canvasVictory.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
