using UnityEngine;

public class Cube : MonoBehaviour
{
    public static bool selectionMode = false;
    private Renderer rend;
    private int row;
    private int col;

    void Start()
    {
        rend = GetComponent<Renderer>();
        row = (int)-gameObject.transform.position.z;
        col = (int)gameObject.transform.position.x;
    }

    void OnMouseDown()
    {
        if (WaveSpawning.placedTowers[row, col])
            return;
        if (selectionMode && WaveSpawning.m.UpdateMoney(-TowerButton.publicPrice))
        {
            selectionMode = false;
            GameObject instance = Instantiate(TowerButton.tower);
            instance.transform.position = transform.position + new Vector3(0, 0.875f, 0);
            instance.name = TowerButton.publicName;
            instance.AddComponent<Tower>();
            WaveSpawning.placedTowers[row, col] = true;
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
