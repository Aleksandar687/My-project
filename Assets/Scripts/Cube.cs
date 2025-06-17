using UnityEngine;

public class Cube : MonoBehaviour
{
    public static bool selectionMode = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        if (selectionMode && WaveSpawning.m.UpdateMoney(-TowerButton.publicPrice))
        {
            selectionMode = false;
            GameObject instance = Instantiate(TowerButton.tower);
            instance.transform.position = transform.position + new Vector3(0, 0.875f, 0);
            instance.name = "Sentry";
            instance.AddComponent<Tower>();
            rend.material.color = Color.white;
        }
    }

    void OnMouseEnter()
    {
        if (selectionMode)
            rend.material.color = Color.grey;
    }

    void OnMouseExit()
    {
        if (selectionMode)
            rend.material.color = Color.white;
    }
}
