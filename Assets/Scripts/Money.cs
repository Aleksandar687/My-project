using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private int m = 100;
    public TMP_Text mText;

    public bool UpdateMoney(int amount)
    {
        if (-amount > m)
            return false;
        m += amount;
        mText.text = "$" + m;
        return true;
    }
}
