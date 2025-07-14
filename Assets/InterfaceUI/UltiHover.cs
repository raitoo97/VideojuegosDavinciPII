using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UltimateHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillCategory skillCategory;

    public void OnPointerEnter(PointerEventData data)
    {
        UltiHoverHandler.instance.ShowPanel(skillCategory);
    }

    public void OnPointerExit(PointerEventData data)
    {
        UltiHoverHandler.instance.HidePanel();
    }

}
