using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour, IDamageable, IBuyable
{
    public int _health = 5;
    public int _attack = 5;
    public int _maxHeight = 3;
    public Text textHealth;

    private int _price = 300;
    public bool isGrounded { get; set; } = false;

    private void Start()
    {
        textHealth.text = "" + _health;
    }

    private void Update()
    {
        if (gameObject.transform.position.y - (gameObject.transform.localScale.y) <= 0) {
            isGrounded = true;
        }

        if (!isGrounded) {
            gameObject.transform.position += new Vector3(0,-0.1f,0);    
        }

        textHealth.text = "" + _health;
    }

    public void AddDamage(int damage)
    {
        _health -= damage;
        if (_health < 0)
        {
            destroy();
        }
    }

   
    void OnCollisionStay2D(Collision2D col)
    {
        if (!col.transform.name.ToLower().Contains("trump") && !col.transform.name.ToLower().Contains("wall"))
        {
            var attackObject = col.transform.GetComponent<IDamageable>();
            if (attackObject != null)
            {
                attackObject.AddDamage(_attack);
                AddDamage(attackObject.GetAttackPower());
            }
        }
    }

    public void AddHealth(int health)
    {
        _health += health;
    }

    public bool CanAffordNew(int moneyInWallet)
    {
        return moneyInWallet >= _price;
    }

    public int BuyNew(int moneyInWallet)
    {
        return moneyInWallet - _price;
    }

    public bool CanAffordUpgrade(int moneyInWallet)
    {
        return moneyInWallet >= _price && isGrounded;
    }

    public int BuyUpgrade(int moneyInWallet)
    {

        if (transform.localScale.y < _maxHeight)
        {
            transform.localScale += new Vector3(0, transform.localScale.y, 0);
            _health *= 2;
            _attack *= 2;
            float y = textHealth.transform.localScale.y / 2;
            textHealth.transform.localScale = new Vector3(textHealth.transform.localScale.x, y, textHealth.transform.localScale.z);
            return moneyInWallet - _price;
        }
        return moneyInWallet;
    }


    public void destroy()
    {
        Destroy(gameObject);
    }

    public int GetAttackPower()
    {
        return _attack;
    }

    public int GetHealth()
    {
        return _health;
    }
}
