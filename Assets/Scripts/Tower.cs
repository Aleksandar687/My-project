using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public GameObject tower;
    private float health;
    private float maxHealth;
    private float damage;
    private float attackSpeed;
    private float range = 5f;
    private short resistance;
    private List<string> qualities;
    private bool active;

    /*rotation  */
    public float rotationSpeed = 10f;
    public List<Transform> targetsInRange = new();
    
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
    
    public void Update()
    {
        FindTargets();
        RotateTowardsTarget();
    }
    private void FindTargets()
    {
      
        targetsInRange.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                targetsInRange.Add(col.transform);
            }
        }
    }

     private void RotateTowardsTarget()
     {
        if (targetsInRange.Count > 0)
        {
            Transform target = targetsInRange[0];
            Vector3 direction = target.position - tower.transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            tower.transform.rotation = Quaternion.Slerp(
                tower.transform.rotation,
                lookRotation,
                Time.deltaTime * rotationSpeed
            );
        }
     }
    
}