using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ChestItemAmount
{
    public int amount;
    public Item prefab;
}

public enum ChestType { Base, Big, Bigger, theBiggest }
[CreateAssetMenu(fileName = "Chest", menuName = "Chest")]
public class ChestObject : ScriptableObject
{
    [Header("�������������� ������������ �������")]
    [Header("��� �������")]
    public ChestType type;

    [Header("�������� ��������")]
    public string name;

    [Header("������ ��������")]
    public GameObject chestPrefab;

    [Header("������ �������� ���������")]
    public List<ChestItemAmount> item;

    [Header("������������ ���-�� �������� � �������")]
    public int maxAmount;

    [Header("�������� ��������")]
    public string aboutChest;

    [Header("������ ��������")]
    public Sprite icon;

}
