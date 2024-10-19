using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDetection : MonoBehaviour
{
    EnemyBow enemyBow;
    EnemyStatic enemyStatic;
    // Start is called before the first frame update
    private void Awake()
    {

        enemyBow = GetComponentInParent<EnemyBow>();
        enemyStatic = GetComponentInParent<EnemyStatic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") )
        {
            if (enemyStatic != null)
            {
                enemyStatic.isInRange = true;
            }
            if(enemyBow != null) 
            {
                enemyBow.isInRange = true;
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemyStatic != null)
            {
                enemyStatic.isInRange = false;
            }
            if (enemyBow != null)
            {
                enemyBow.isInRange = false;
            }
        }
    }


}
