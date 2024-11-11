using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofGround : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canMove;
    public bool isFake;

    private Vector3 startPosition;

    float distanciadeRecorrido = 3f;      
    float velocidad = 3f;

    public float tiempoParaComenzar;

    void Start()
    {
        startPosition = transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            float yOffset = Mathf.PingPong((Time.time + tiempoParaComenzar) * velocidad, distanciadeRecorrido * 2) - distanciadeRecorrido;
            transform.position = startPosition + new Vector3(0, yOffset, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && isFake == true)
        {
            //transform.position = startPosition + new Vector3(0, yOffset, 0);

        }
    }

 
}
