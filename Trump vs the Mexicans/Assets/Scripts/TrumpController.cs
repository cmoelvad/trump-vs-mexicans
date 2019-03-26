using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        print(horizontal);
        Move(horizontal);
    }

    private void Move(float move)
    {
        animator.SetFloat("speed", move);

        rb2d.velocity = new Vector2(move * movementSpeed, rb2d.velocity.y);

        if(move > 0 && !facingRight || move < 0 && facingRight)
        {
            print("flip");
            flip();
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
