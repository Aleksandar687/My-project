using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveSpawning : MonoBehaviour
{
    private int count = 0;
    private Coroutine spawnRoutine;
    private List<byte[]> setVariants = new List<byte[]>{
        new byte[] { 0, 0, 1, 0, 0 },
        new byte[] { 0, 0, 1, 0, 0 },
        new byte[] { 0, 1, 0, 1, 0 },
        new byte[] { 0, 1, 0, 1, 0 },
        new byte[] { 1, 0, 0, 0, 1 },
        new byte[] { 0, 1, 1, 1, 0 },
        new byte[] { 1, 0, 1, 0, 1 },
        new byte[] { 0, 1, 1, 1, 0 },
        new byte[] { 1, 1, 0, 1, 1 },
        new byte[] { 0, 0, 2, 0, 0 },
        new byte[] { 1, 1, 1, 1, 1 },
        new byte[] { 0, 1, 2, 1, 0 },
        new byte[] { 1, 0, 2, 0, 1 },
        new byte[] { 0, 2, 0, 2, 0 },
        new byte[] { 1, 1, 2, 1, 1 },
        new byte[] { 0, 2, 2, 2, 0 },
        new byte[] { 0, 1, 1, 1, 0 },
        new byte[] { 2, 0, 2, 0, 2 },
        new byte[] { 0, 0, 2, 0, 0 },
        new byte[] { 0, 0, 3, 0, 0 }
    };
    private List<byte[]> rndVariants = new List<byte[]>{
        new byte[] { 0, 1, 0, 1, 0 },
        new byte[] { 1, 0, 0, 0, 1 },
        new byte[] { 0, 1, 0, 1, 0 },
        new byte[] { 1, 0, 1, 0, 1 },
        new byte[] { 0, 1, 1, 1, 0 }
    };
    public GameObject enemyC;
    public GameObject label;
    public static Money m;

    void Start()
    {
        m = gameObject.GetComponent<Money>();
        spawnRoutine = StartCoroutine(SpawnSetWave());
    }

    private System.Collections.IEnumerator SpawnSetWave()
    {
        while (true)
        {
            byte[] temp = setVariants[count];
            m.UpdateMoney(count * 5);
            count++;
            label.GetComponent<TMP_Text>().text = "Wave " + count;
            for (int i = 0; i < 5; i++)
                if (temp[i] == 1)
                {
                    GameObject instance = Instantiate(enemyC);
                    instance.transform.position = new Vector3(8.5f, 1.26f, 0 - i);
                    instance.name = "Zombie";
                    instance.AddComponent<Enemy>();
                }
                if (count < 20) yield return new WaitForSeconds(4);
                else
                {
                    StopCoroutine(spawnRoutine);
                    spawnRoutine = StartCoroutine(SpawnRndWave());
                }
        }
    }

    private System.Collections.IEnumerator SpawnRndWave()
    {
        while (true)
        {
            byte[] temp = rndVariants[(int)Mathf.Floor(Random.Range(0, 4))];
            for (int i = 0; i < 5; i++)
                if (temp[i] == 1)
                {
                    GameObject instance = Instantiate(enemyC);
                    instance.transform.position = new Vector3(8.5f, 1.26f, 0 - i);
                    instance.name = "Zombie";
                    instance.AddComponent<Enemy>();
                }
                yield return new WaitForSeconds(6);
        }
    }
}