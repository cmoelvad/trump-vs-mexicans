using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour, IDamageable, IBuyable
{
    public int _health = 5;
    public int _attack = 5;
    public int _maxHeight = 3;
    private int _price = 25;
    
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
        return moneyInWallet >= _price;
    }

    public int BuyUpgrade(int moneyInWallet)
    {

        if (transform.localScale.y < _maxHeight)
        {
            transform.localScale += new Vector3(0, transform.localScale.y, 0);
            _health *= 2;
            _attack *= 2;
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
}
