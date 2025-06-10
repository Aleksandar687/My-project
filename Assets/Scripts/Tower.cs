using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Tower : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private float damage;
    private float attackSpeed;
    private float range;
    private short resistance;
    private List<string> qualities;
    private bool active;
    private GameObject target;
    private Coroutine shootRoutine;
    private bool shooting = false;

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }

    public Tower()
    {
        health = 100;
        maxHealth = 100;
        damage = 40;
        attackSpeed = 0.7f;
        range = 5;
        resistance = 0;
        active = true;
    }

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
        FindTarget();
        if (target is not null)
        {
            RotateTowardsTarget();
            if (!shooting)
            {
                shooting = true;
                shootRoutine = StartCoroutine(RepeatShoot());
            }
        }
        else if (shooting)
        {
            shooting = false;
            StopCoroutine(shootRoutine);
        }
    }

    private IEnumerator<WaitForSeconds> RepeatShoot()
    {
        DealDamage(target.GetComponent<Enemy>());
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);
            DealDamage(target.GetComponent<Enemy>());
        }
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in colliders.OrderBy(x => x.gameObject.transform.position.x))
            if (col.CompareTag("Enemy"))
            {
                target = col.gameObject;
                return;
            }
        target = null;
    }

    private void RotateTowardsTarget()
    {
        Transform top = transform;
        foreach (Transform i in transform)
            if (i.gameObject.name == "Top")
            {
                top = i;
                break;
            }
        Vector3 direction = target.transform.position - top.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        top.rotation = Quaternion.Slerp(top.rotation, lookRotation, Time.deltaTime * 10);
    }
    
}