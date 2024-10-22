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
    Vector3 offset = new Vector3(0,1.5f,0);

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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {

            playerToInvoke.transform.position = lastPlayerPosition;
            Destroy(lastPlayer);
            Instantiate(playerToInvoke, lastPlayerPosition + offset, Quaternion.identity);
        }
    }
}
