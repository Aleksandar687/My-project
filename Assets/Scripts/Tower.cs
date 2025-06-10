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

    /*rotation  */
    public float range = 5f;
    public float rotationSpeed = 10f;
    public Transform towerfront ;


      [Header("Targeting")]
    public List<Transform> targetsInRange = new List<Transform>();
    public LayerMask enemyLayer;
    
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
    {   FindTargets();
        RotateTowardsTarget();
    }
    private void FindTargets()
    {
      
        targetsInRange.Clear();
        
    
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyLayer);
        
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
            // Get the first target (you can modify this to prioritize different targets)
            Transform target = targetsInRange[0];
            
            Vector3 direction = target.position - rotatingPart.position;
            direction.y = 0; // Keep rotation only on Y axis if it's a 3D game
            
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rotatingPart.rotation = Quaternion.Slerp(
                rotatingPart.rotation, 
                lookRotation, 
                Time.deltaTime * rotationSpeed
            );
        }
    }
    
}
}
