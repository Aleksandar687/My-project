using UnityEngine;
using TMPro;

public class WaveSpawning : MonoBehaviour
{
    private int count;

    void Start()
    {
        count = 0;
        InvokeRepeating("MyMethod", 0f, 3f);
    }

    void MyMethod()
    {
        count++;
        GameObject.Find("WaveCounter").GetComponent<TMP_Text>().text = "Wave " + count;
    }
}