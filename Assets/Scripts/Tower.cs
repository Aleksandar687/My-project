using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class Tower : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private float damage;
    private float attackSpeed;
    private float range;
    private short resistance;
    private List<string> qualities;
    private GameObject target;
    private Coroutine shootRoutine;
    private bool shooting = false;
    private GameObject canvas;
    private GameObject tempBullet;

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }

    private void Start()
    {
        switch (gameObject.name)
        {
            case "Sentry":
                health = 5;
                maxHealth = 5;
                damage = 3;
                attackSpeed = 0.9f;
                range = 3;
                break;
            case "Sniper":
                health = 3;
                maxHealth = 3;
                damage = 6;
                attackSpeed = 1.6f;
                range = 6;
                break;
            case "Rapidfire":
                health = 8;
                maxHealth = 8;
                damage = 1.5f;
                attackSpeed = 0.4f;
                range = 2;
                break;
            default:
                break;
        }
        resistance = 0;
        attackSpeed += Random.Range(0, 0.01f);
        canvas = transform.Find("Canvas").gameObject;
        canvas.transform.Find("Health").GetComponent<TMP_Text>().text = health + "/" + maxHealth;
    }
    
    private void Update()
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.collider.gameObject == gameObject)
            {
                canvas.gameObject.SetActive(true);
                canvas.transform.LookAt(Camera.main.transform);
                canvas.transform.rotation = Quaternion.Euler(0f, canvas.transform.rotation.eulerAngles.y + 180f, 0f);
                return;
            }
        canvas.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator RepeatShoot()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        while (true)
        {
            try
            {
                enemy.TakeDamage(damage);
                tempBullet = Instantiate(gameObject.transform.Find("Top").Find("Bullet").gameObject);
                tempBullet.transform.position = gameObject.transform.Find("Top").Find("Bullet").position;
                StartCoroutine(MoveBullet());
            }
            catch (MissingReferenceException)
            {
                StopCoroutine(shootRoutine);
                shooting = false;
                break;
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    System.Collections.IEnumerator MoveBullet()
    {
        Vector3 p = target.transform.position;
        Destroy(tempBullet, 0.39f);
        while (Vector3.Distance(tempBullet.transform.position, p) > 0.1f)
        {
            tempBullet.transform.position = Vector3.MoveTowards(tempBullet.transform.position, p, 50 * Time.deltaTime);
            yield return null;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (dmg * Resistance >= health)
        {
            Death();
            return;
        }
        health -= dmg * Resistance;
        canvas.transform.Find("Health").GetComponent<TMP_Text>().text = health + "/" + maxHealth;
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

    public void Death()
    {
        int row = (int)-gameObject.transform.position.z;
        int col = (int)gameObject.transform.position.x;
        WaveSpawning.placedTowers[row, col] = false;
        foreach (var i in gameObject.scene.GetRootGameObjects())
        {
            if (i.name == "Zombie" && i.transform.position.z == transform.position.z)
                i.GetComponent<Enemy>().Target = null;
        }
        Destroy(gameObject);
    }
}