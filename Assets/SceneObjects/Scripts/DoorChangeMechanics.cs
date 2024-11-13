using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorChangeMechanics : MonoBehaviour
{
    BoxCollider doorCollider;

    [SerializeField] GameObject playerToInvoke;
    GameObject lastPlayer;

    Vector3 lastPlayerPosition;
    Quaternion lastPlayerRotation;

    [SerializeField] GameObject canvasVictory;


    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
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
            if(gameObject.name == "PuertaFinal")
            {
                Time.timeScale = 0f;
                canvasVictory.SetActive(true);
                SceneManager.LoadScene("Level1");
            }
            else
            {
                Destroy(lastPlayer);
                Instantiate(playerToInvoke, lastPlayerPosition, lastPlayerRotation);
            }
           
        }
    }
}