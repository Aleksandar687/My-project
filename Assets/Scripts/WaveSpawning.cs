using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveSpawning : MonoBehaviour
{
    private int count = 0;
    private List<bool[]> variants = new List<bool[]>{
        new bool[] { true, false, true, false, true },
        new bool[] { false, true, true, true, false },
        new bool[] { false, true, false, true, false },
        new bool[] { false, false, true, false, false }
    };
    public GameObject enemyC;
    public GameObject label;

    void Start()
    {
        InvokeRepeating("SpawnWave", 0, 4);
    }

    void SpawnWave()
    {
        count++;
        label.GetComponent<TMP_Text>().text = "Wave " + count;
        bool[] temp = variants[(int)Mathf.Floor(Random.Range(0, 4))];
        for (int i = 0; i < 5; i++)
            if (temp[i])
            {
                GameObject instance = Instantiate(enemyC);
                instance.transform.position = new Vector3(8, 1.26f, 0 - i);
                instance.name = "Zombie";
                Enemy enemyScript = instance.AddComponent<Enemy>();
            }
    }
}