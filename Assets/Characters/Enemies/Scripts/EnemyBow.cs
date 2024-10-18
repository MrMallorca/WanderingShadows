using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : MonoBehaviour
{
    Vector3 offset;
    float arrowForce = 500f;

    public GameObject arrowPrefab;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-1, 1.3f, 0);

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shooting()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position + offset, transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * arrowForce);
        Destroy(arrow,2f);

    }

  
}
