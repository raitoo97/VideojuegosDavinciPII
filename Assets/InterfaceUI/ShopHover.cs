using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float cost;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopHoverHandler.instance.ShowPanel(cost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShopHoverHandler.instance.HidePanel();
    }
}
