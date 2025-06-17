using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private float damage;
    private float walkspeed;
    private short resistance;
    private bool isMoving = true;
    private Coroutine attackRoutine;
    private GameObject target = null;
    private GameObject canvas;

    public float Health { get => health; }
    public float Resistance { get => 1 - (float)resistance / 100; }
    public GameObject Target { set => target = value; }

    public Enemy()
    {
        health = 10;
        maxHealth = 10;
        damage = 1;
        walkspeed = 1;
        resistance = 0;
    }

    private void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        canvas.transform.Find("Health").GetComponent<TMP_Text>().text = health + "/" + maxHealth;
    }

    private void Update()
    {
        if (isMoving)
            transform.position += new Vector3(-1, 0, 0) * walkspeed * Time.deltaTime;
        else if (target is null)
        {
            StopCoroutine(attackRoutine);
            target = null;
            isMoving = true;
        }

        if (transform.position.x < 0)
            Death();

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

    private System.Collections.IEnumerator RepeatAttack()
    {
        Tower tower = target.GetComponent<Tower>();
        try
        {
            tower.TakeDamage(damage);
        }
        catch (MissingReferenceException)
        {
            StopCoroutine(attackRoutine);
            target = null;
            isMoving = true;
        }
        while (true)
        {
            yield return new WaitForSeconds(1);
            try
            {
                tower.TakeDamage(damage);
            }
            catch (MissingReferenceException)
            {
                StopCoroutine(attackRoutine);
                target = null;
                isMoving = true;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Tower") && isMoving)
        {
            isMoving = false;
            target = collider.gameObject;
            attackRoutine = StartCoroutine(RepeatAttack());
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

    public void Death()
    {
        WaveSpawning.m.UpdateMoney(15);
        Destroy(gameObject);
    }
}
