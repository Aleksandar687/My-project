using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public static GameObject tower;
    public static int publicPrice;
    public int towerPrice;

    public void Press()
    {
        if (Cube.selectionMode)
            Cube.selectionMode = false;
        else
        {
            publicPrice = towerPrice;
            Cube.selectionMode = true;
            tower = gameObject.transform.Find("Tower").gameObject;
        }
    }
}