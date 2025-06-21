using UnityEngine;

public class Bubble : MonoBehaviour
{
    private bool claimed = false;
    
    private void Start()
    {
        StartCoroutine(AutoClaim());
    }

    System.Collections.IEnumerator AutoClaim()
    {
        yield return new WaitForSeconds(4 + Random.Range(0, 1));
        if (!claimed) Claim();
    }

    public void OnMouseEnter()
    {
        if (!claimed)
            Claim();
    }

    private void Claim()
    {
        WaveSpawning.m.UpdateMoney(5);
        claimed = true;
        Destroy(gameObject);
    }
}
