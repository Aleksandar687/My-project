using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private float damage;
    private float walkspeed;
    private short resistance;
    private bool active;
    private bool isMoving = true;

    private void Update()
    {
        if (isMoving)
            transform.position += new Vector3(-1, 0, 0) * walkspeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("collide"))
        {
            isMoving = false;
            
        }
    }

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }

    public Enemy()
    {
        health = 100;
        maxHealth = 100;
        damage = 5;
        walkspeed = 0.5f;
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
