using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;

    private Vector3 generatorPos;

    private void Start()
    {
        generatorPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void RandomSpawner()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 20), 0, -0.8f);

        Instantiate(obstacle, generatorPos + randomSpawnPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("RandomSpawner", 0f, 1.5f);
        }

    }
}
