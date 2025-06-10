using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        Debug.Log("Button clicked!");
        
    }
}