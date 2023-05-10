using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillSlot : MonoBehaviour
{
    [Header("��������������")]
    [SerializeField] private Image skillIcon;

    public void SetIcon(Sprite icon)
    {
        skillIcon.sprite = icon;
    }
}
