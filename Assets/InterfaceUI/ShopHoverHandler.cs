using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHoverHandler : MonoBehaviour
{
    public static ShopHoverHandler instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text text;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void ShowPanel(float cost)
    {
        text.text = "Pay the cost: " + cost + " to unlock skills";
        panel.SetActive(true);
    }

    public void HidePanel() 
    {
        panel.SetActive(false);
    }

}
