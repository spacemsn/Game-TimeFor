using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Element")]
public class ElementObject : Item
{
    [Header("��� ������")]
    public Elements element;

    [Header("��� ������")]
    public float baseDamage;

    [Header("��� ������")]
    public float basePersent;

    public float Formule()
    {

        return 0;
    }
}

