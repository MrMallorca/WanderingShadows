using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;

    private Vector3 generatorPos;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
          generatorPos = transform.position;
    }

    private void RandomSpawner()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 20), 0, 0);

        Instantiate(obstacle, generatorPos + randomSpawnPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("RandomSpawner", 0f, 0.5f);
        }

    }
}
