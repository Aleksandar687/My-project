using UnityEngine;
using System.Collections.Generic;

public class Tower
{
    private float health;
    private float maxHealth;
    private float damage;
    private float attackSpeed;
    private float range;
    private short resistance;
    private List<string> qualities;
    private bool active;

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }

    public void DealDamage(Enemy enemy)
    {
        if (damage * enemy.Resistance >= enemy.Health) enemy.Death();
        else enemy.TakeDamage(damage);
    }

    public void TakeDamage(float dmg) => health -= dmg * Resistance;

    public void Death()
    {
        active = false;
    }
}
