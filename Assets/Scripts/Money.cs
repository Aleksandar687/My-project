using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public int cash = 100;
    public TMP_Text mText;

    public bool UpdateMoney(int amount)
    {
        if (-amount > cash)
            return false;
        cash += amount;
        mText.text = "$" + cash;
        return true;
    }
}
