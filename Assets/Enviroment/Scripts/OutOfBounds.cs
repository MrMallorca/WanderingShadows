using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{

    static public OutOfBounds instance;

    [SerializeField] GameObject lastPlayer;
    [SerializeField] CheckPoint lastCheckPoint;
    CharacterController playerCharacterController;
     

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
        playerCharacterController.enabled = false;
        lastPlayer.transform.position =
                lastCheckPoint.transform.position;
        playerCharacterController.enabled = true;

    }
}
