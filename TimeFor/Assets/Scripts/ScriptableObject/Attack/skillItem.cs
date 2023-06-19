using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Object/ItemObject/Attack")]
public class skillItem : Item
{
    [Header("�������������� ������")]
    [Header("������")]
    public Elements element;
    [Header("������������� ������")]
    public ElementObject elementObject;
    [Header("����� �����������")]
    public float attackRollback;
    [Header("��������")]
    public float speed;
}
