using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorChangeMechanics : MonoBehaviour
{
    BoxCollider doorCollider;

    [SerializeField] GameObject playerToInvoke;
    GameObject lastPlayer;

    Vector3 lastPlayerPosition;
    Quaternion lastPlayerRotation;


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
            Destroy(lastPlayer);
            Instantiate(playerToInvoke, lastPlayerPosition , lastPlayerRotation);
        }
    }
}
