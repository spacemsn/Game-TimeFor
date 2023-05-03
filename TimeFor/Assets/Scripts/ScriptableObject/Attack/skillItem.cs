using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Object/ItemObject/Attack")]
public class skillItem : Item
{
    [Header("�������������� ������")]
    [Header("����")]
    public float damage;
    [Header("����� �����������")]
    public float attackRollback;
    [Header("��������")]
    public float speed;
}
