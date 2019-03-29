using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform PrefabToSpawn;
    private float spawnTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        //InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    public void SpawnEnemy()
    {
        Instantiate(PrefabToSpawn, position: gameObject.transform.position, new Quaternion());
    }
}
