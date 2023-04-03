using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Object/ItemObject/Attack")]
public class SkillObject : ItemObject
{
    [Header("�������������� ������")]
    [Header("����������� ����")]
    public int consumption;
    [Header("����")]
    public float damage;
    [Header("����� �����������")]
    public float attackRollback;
    [Header("��������")]
    public float speed;
}
