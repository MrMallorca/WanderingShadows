using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : MonoBehaviour
{


    Animator anim;

    public bool isInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isInRange)
        {
            anim.SetBool("PlayerSpotted", true);
        }
        else
        {
            anim.SetBool("PlayerSpotted", false);
        }
    }
    
}
