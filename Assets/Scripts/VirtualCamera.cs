using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    GameObject currentPlayer;
    Transform currentPlayerPos;
    CinemachineVirtualCamera cineMachineCamera;
    // Start is called before the first frame update
    void Start()
    {
        cineMachineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");

        currentPlayerPos = currentPlayer.transform;
        cineMachineCamera.Follow = currentPlayerPos;
    }
}
