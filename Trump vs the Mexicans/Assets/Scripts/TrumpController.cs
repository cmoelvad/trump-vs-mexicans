using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpController : MonoBehaviour, IDamageable, IWallet
{
    public float movementSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool facingRight = false;
    public Transform PrefabToBuild;
    public int money;
    public int health;
    public float jumpForce = 20f;
    public LayerMask whatIsGround;

    const float GROUND_CHECK_RADIUS = .2f;
    private Transform groundCheck;
    private bool grounded;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("groundCheck");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnPrefab();
        }

        if (!jumping)
        {
            jumping = Input.GetButtonDown("Jump");
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        var buyObject = col.transform.GetComponent<IBuyable>();
        var attackObject = col.transform.GetComponent<IDamageable>();

        if (buyObject != null )
        {
            if (buyObject.CanAffordUpgrade(money))
            {
                money = buyObject.BuyUpgrade(money);
            }
            
        } else if (attackObject != null)
        {
            AddDamage(attackObject.GetAttackPower());
        }

    }

    private void SpawnPrefab()
    {
        var newWall = Instantiate(PrefabToBuild);
        newWall.position += gameObject.transform.position + new Vector3(0, 5, 0);

        var itemToBuy = newWall.GetComponent<IBuyable>() ;
        if (itemToBuy != null)
        {

            if (!itemToBuy.CanAffordNew(money))
            {
             
                Destroy(newWall.gameObject);
            }
            else
            {
             
                money = itemToBuy.BuyNew(money);
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Move(horizontal, jumping);
        jumping = false;

        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GROUND_CHECK_RADIUS, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            grounded = true;
        }
    }

    private void Move(float move, bool jump)
    {
        animator.SetFloat("speed", move);

        rb2d.velocity = new Vector2(move * movementSpeed, rb2d.velocity.y);

        if(move > 0 && !facingRight || move < 0 && facingRight)
        {
            //print("flip");
            flip();
        }

        if (grounded && jump)
        {

            grounded = false;
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void AddDamage(int damage)
    {
        health -= damage;

        if (health < 0)
        {
            KillTrump();
        }
    }

    private void KillTrump()
    {
        GameController.Restart();
        Destroy(gameObject);
    }

    public void AddHealth(int health)
    {
        this.health += health;
    }

    public int GetAttackPower()
    {
        return 0;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddMoney(int money)
    {
        this.money += money;
    }

    public int GetMoney()
    {
        return money;
    }
}
