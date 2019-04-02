using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public Transform PrefabToSpawn;
    private int numberOfSpawnedEnemies;
    private int numberOfCurrentEnemies;
    private int currentEnemiesInThisWave = 1;
    public bool isAllowedToSpawn = true;
    private int enemyHealth;
    private int waveNumber;

    public int healthToAdd;
    public int attackPowerToAdd;
    private double moneyWorthToAddInPercent = 1;

    public Text health_Text;
    public Text attackPower_Text;
    public Text moneyworth_Text;

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
        numberOfCurrentEnemies = FindObjectsOfType<EnemyController>().Length;

        if (numberOfCurrentEnemies > waveSpawn) {
            isAllowedToSpawn = false;
        } else if (numberOfCurrentEnemies == 0) {
            currentEnemiesInThisWave *= 2;

            healthToAdd = (int)(enemyHealth * (0.10 * waveNumber));
            print("result after int: " + healthToAdd);
            attackPowerToAdd++;
            moneyWorthToAddInPercent += 0.6;
            waveNumber++;

            numberOfSpawnedEnemies = 0;
            isAllowedToSpawn = true;
            waveSpawn = (int) (waveSpawn * 1.3);

            while (numberOfCurrentEnemies <= currentEnemiesInThisWave && !(numberOfCurrentEnemies >  waveSpawn))
            {
                SpawnEnemy();
                numberOfCurrentEnemies = FindObjectsOfType<EnemyController>().Length;
            }


        }

       

    }

    public void SpawnEnemy()
    {
        if (isAllowedToSpawn) {
            numberOfSpawnedEnemies++;
            var enemy = Instantiate(PrefabToSpawn, position: gameObject.transform.position, new Quaternion());
            var enemyController = enemy.GetComponent<EnemyController>();

            enemyController.AddHealth(healthToAdd);
            enemyController.AddAttackPower(attackPowerToAdd);
            enemyController.AddPercentToMoneyWorth(moneyWorthToAddInPercent);

            health_Text.text = enemyController.GetHealth() + "";
            attackPower_Text.text = enemyController.GetAttackPower() + "";
            moneyworth_Text.text = enemyController.GetMoneyWorth() + "";
            enemyHealth = enemyController.GetHealth();
        }

    }
}
