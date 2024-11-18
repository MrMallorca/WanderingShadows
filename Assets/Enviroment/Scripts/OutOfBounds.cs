using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{

    static public OutOfBounds instance;

    [SerializeField] GameObject lastPlayer;
    [SerializeField] CheckPoint lastCheckPoint;
    CharacterController playerCharacterController;

    [SerializeField] GameLogic gameLogicS;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        if (instance != null)
        {
            throw new System.Exception(
                "Error");
        }
        instance = this;

    }

    private void Update()
    {
        lastPlayer = GameObject.FindGameObjectWithTag("Player");
        playerCharacterController = lastPlayer.GetComponent<CharacterController>();
    }

    public void NotifyCheckPointTriggered(CheckPoint checkPoint)
    {
        lastCheckPoint = checkPoint;
    }

    

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(lastCheckPoint == null) 
        {
            StartCoroutine(gameLogicS.ReloadScene());
        }
        else if (col.gameObject ==  lastPlayer)
        {
            playerCharacterController.enabled = false;
            lastPlayer.transform.position =
                    lastCheckPoint.transform.position;
            playerCharacterController.enabled = true;
        }

    }
}
