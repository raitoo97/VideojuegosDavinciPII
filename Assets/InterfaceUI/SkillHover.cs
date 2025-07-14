using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UltiHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillCategory skillCategory;
    public SkillStatType stat;

    public void OnPointerEnter(PointerEventData data)
    {
        SkillHoverHandler.instance.ShowPanel(skillCategory, stat);
    }

    public void OnPointerExit(PointerEventData data) 
    {
        SkillHoverHandler.instance.HidePanel();
    }

}
