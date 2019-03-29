using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int health;
    private int attackPower = 1;
    public int moneyWorth;
   public IWallet ToGiveMoneyTo;

    private void GiveRewards()
    {
        Transform[] objects = GameObject.FindObjectsOfType<Transform>();
        foreach(Transform ObjectInArray in objects)
        {
            var wallet = ObjectInArray.GetComponent<IWallet>();
            if (wallet != null)   
            {
                wallet.AddMoney(moneyWorth);
            }
            
        }
    }
    public void AddDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            GiveRewards();
            SpawnEnemy();
            SpawnEnemy();

            Destroy(gameObject);
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawn spawn = GameObject.FindObjectOfType<EnemySpawn>();
        spawn.SpawnEnemy();

    }

    internal void AddAttackPower(int attackPowerToAdd)
    {
        attackPower += attackPowerToAdd;
    }

    public void AddHealth(int health)
    {
        this.health += health;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0.02f,0, 0);
    }
}
