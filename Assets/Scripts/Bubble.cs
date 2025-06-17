using UnityEngine;

public class Bubble : MonoBehaviour
{
    private bool xpGiven = false;

    
    public void OnHover()
    {
        if (!xpGiven)
        {
            WaveSpawning.m.UpdateMoney(10);
            xpGiven = true;
            Destroy(gameObject);
        }
    }
}
