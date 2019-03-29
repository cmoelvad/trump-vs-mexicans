using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int health;
    private int attackPower = 1;
    private int moneyWorth;
    public IWallet ToGiveMoneyTo;

    private void Start()
    {
        moneyWorth = 20;
    }
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

    public void AddPercentToMoneyWorth(double percentToAdd) {
        print("moneyworth: " + moneyWorth);
        print("percentToAdd: " + percentToAdd);
        moneyWorth = (int) (moneyWorth * percentToAdd);
        print("result: " + moneyWorth);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0.02f,0, 0);
    }
}
