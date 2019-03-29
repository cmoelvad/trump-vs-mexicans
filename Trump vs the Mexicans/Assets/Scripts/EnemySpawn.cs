using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform PrefabToSpawn;
    private int numberOfSpawnedEnemies;
    private int numberOfCurrentEnemies;
    public bool isAllowedToSpawn = true;

    public int healthToAdd;
    public int attackPowerToAdd;
    private double moneyWorthToAddInPercent = 1;

    public int waveSpawn;

    //private float spawnTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        //InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCurrentEnemies = GameObject.FindObjectsOfType<EnemyController>().Length;
        if (numberOfCurrentEnemies > waveSpawn) {
            isAllowedToSpawn = false;
        } else if (numberOfCurrentEnemies == 0) {

            healthToAdd++;
            attackPowerToAdd++;
            moneyWorthToAddInPercent += 0.3;
            print("worh percent is added by 0.3, is now: " + moneyWorthToAddInPercent);

            numberOfSpawnedEnemies = 0;
            isAllowedToSpawn = true;
            waveSpawn = (int) (waveSpawn * 1.3);
            SpawnEnemy();
        }

    }

    public void SpawnEnemy()
    {
        if (isAllowedToSpawn) {
            numberOfSpawnedEnemies++;
            var enemy = Instantiate(PrefabToSpawn, position: gameObject.transform.position, new Quaternion());
            if (numberOfSpawnedEnemies % waveSpawn == 0)
            {
                var enemyController = enemy.GetComponent<EnemyController>();
                enemyController.AddHealth(healthToAdd);
                enemyController.AddAttackPower(attackPowerToAdd);
                enemyController.AddPercentToMoneyWorth(moneyWorthToAddInPercent);

            }
        }

    }
}
