using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {

       OutOfBounds.instance.NotifyCheckPointTriggered(this);


    }
}
