using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveSpawning : MonoBehaviour
{
    private static int count = 0;
    private static List<GameObject> enemies = new();
    public GameObject objectToClone;
    public Vector3 offset = new Vector3(1f, 0f, 0f);

    void Start()
    {
        InvokeRepeating("MyMethod", 0f, 3f);
    }

    void MyMethod()
    {
        count++;
        GameObject.Find("WaveCounter").GetComponent<TMP_Text>().text = "Wave " + count;
        GameObject clone = Instantiate(objectToClone);
        clone.transform.position = objectToClone.transform.position;
        clone.name = objectToClone.name + "_Clone";
        enemies.Add(clone);
        foreach (var e in enemies)
            e.transform.position += offset;
    }
}