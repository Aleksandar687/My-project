using UnityEngine;

public class Enemy
{
    private float health;
    private float maxHealth;
    private float damage;
    private float walkspeed;
    private short resistance;
    private bool active;

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }

    public Enemy()
    {
        health = 100;
        maxHealth = 100;
        damage = 5;
        walkspeed = 1;
        resistance = 0;
        active = true;
    }

    public void DealDamage(Tower tower)
    {
        if (damage * tower.Resistance >= tower.Health) tower.Death();
        else tower.TakeDamage(damage);
    }

    public void TakeDamage(float dmg) => health -= dmg * Resistance;

    public void Death()
    {
        active = false;
    }
}
