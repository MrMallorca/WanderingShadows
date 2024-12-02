using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBow : MonoBehaviour
{
    Vector3 offset;
    float arrowForce = 500f;

    BoxCollider attackRange;

    public GameObject arrowPrefab;
    Animator anim;

    public bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-1, 1.3f, 0);

        anim = GetComponent<Animator>();
        attackRange = GetComponentInParent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            anim.SetBool("isShooting", true);
        }
        else 
        {
            anim.SetBool("isShooting", false);
        }
    }

    public void Shooting()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position + offset, transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * arrowForce);


    }

   
}
