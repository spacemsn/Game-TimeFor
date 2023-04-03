using UnityEngine;

public enum ObjectType { Default, Potion, Attack, Instrument, Enemies }
public class ItemObject: ScriptableObject
{
    [Header("�������������� ������������ ��������")]
    [Header("��� �������")]
    public ObjectType type;
    [Header("�������� ��������")]
    public string name;
    [Header("������ ��������")]
    public GameObject objectPrefab;
    [Header("������������ ���-�� � ���������")]
    public int maxAmount;
    [Header("�������� ��������")]
    public string aboutObject;
    [Header("������ ��������")]
    public Sprite icon;
}
