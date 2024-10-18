using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDetection : MonoBehaviour
{
    EnemyStatic enemy;
    // Start is called before the first frame update
    private void Awake()
    {
        enemy = GetComponentInParent<EnemyStatic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {
    //        enemy.isInRange = true;
    //    }
    //}

    
}
