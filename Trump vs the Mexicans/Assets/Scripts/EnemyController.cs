using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int health;
    private int attackPower = 1;
    private int moneyWorth;
    public IWallet ToGiveMoneyTo;
    private Rigidbody2D rigidbody;
    private float jumpForce = 5;
    private bool grounded;
    public LayerMask whatIsGround;

    const float GROUND_CHECK_RADIUS = .2f;
    public Transform groundCheck;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        //groundCheck = gameObject.GetComponentInChildren<Transform>();
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

    private void FixedUpdate()
    {
        grounded = rigidbody.velocity.y > -.1 && rigidbody.velocity.y < .1;
        var randomNumber = Random.Range(1, 100);
        if (randomNumber == 1 && grounded)
        {
            print(randomNumber + ", " + grounded);
            rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }
    }


}
