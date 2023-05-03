using UnityEngine;

public enum ObjectType { Default, Enemies, Players, NPC }
public class ItemObject : ScriptableObject
{
    [Header("�������������� ������������ ��������")]
    [Header("��� �������")]
    public ObjectType type;

    [Header("�������� ��������")]
    public string name;

    [Header("������ ��������")]
    public GameObject objectPrefab;

    [Header("�������� ��������")]
    public string aboutObject;

    [Header("������ ��������")]
    public Sprite icon;

}
