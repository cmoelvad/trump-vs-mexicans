using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void AddDamage(int damage);
    void AddHealth(int health);
    int GetAttackPower();
    
}
