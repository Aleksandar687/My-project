using UnityEngine;

public class Bubble : MonoBehaviour
{
    private bool xpGiven = false;

    
    public void OnHover()
    {
        if (!xpGiven)
        {
            XPManager.Instance.GiveXP(5); // Give XP
            xpGiven = true;
            Destroy(gameObject); // Remove the bubble
        }
    }
}
