using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("�������������� �����")]
    [Header("�������� �����")]
    public float hp;
    [Header("���� �����")]
    public int enemyDamage;
    [Header("���� ����������� �����")]
    public float viewAngle;
    [Header("���������� ����������� �����")]
    public float viewDistance;
}
