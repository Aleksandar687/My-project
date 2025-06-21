using UnityEngine;

public class Bubble : MonoBehaviour
{
    private bool xpGiven = false;
    
    public void OnMouseEnter()
    {
        if (!xpGiven)
        {
            WaveSpawning.m.UpdateMoney(5);
            xpGiven = true;
            Destroy(gameObject);
        }
    }
}
