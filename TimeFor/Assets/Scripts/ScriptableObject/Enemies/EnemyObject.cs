using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("�������������� �����")]
    [Header("�������� �����")]
    [SerializeField] private float hp;

    [Header("���� �����")]
    [SerializeField] private int enemyDamage;

    [Header("���� ����������� �����")]
    [SerializeField] private float viewAngle;

    [Header("���������� ����������� �����")]
    [SerializeField] private float viewDistance;

    public void SetDamage(EnemyDamage enemy)
    {
        enemy.hp = hp;
        enemy.enemyDamage = enemyDamage;
    }

    public void SetBehavior(EnemyBehavior enemy)
    {
        enemy.viewAngle = viewAngle;
        enemy.viewDistance = viewDistance;
    }
}
