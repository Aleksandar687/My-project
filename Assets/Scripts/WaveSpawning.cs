using UnityEngine;

public class WaveSpawning : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("MyMethod", 0f, 3f); // Start after 1s, repeat every 2s
    }

    void MyMethod()
    {
        Debug.Log("Method called periodically");
    }
}