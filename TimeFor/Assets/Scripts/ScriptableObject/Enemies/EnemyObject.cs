using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Object/ItemObject/Enemies")]
public class EnemyObject : ItemObject
{
    [Header("�������������� �����")]
    [Header("�������� �����")]
    [SerializeField] private float hp;
    [SerializeField] private float baseHP;

    [Header("���� �����")]
    [SerializeField] private int enemyDamage;
    [SerializeField] private int baseDamage;

    [Header("���� ����������� �����")]
    [SerializeField] private float viewAngle;

    [Header("���������� ����������� �����")]
    [SerializeField] private float viewDistance;

    public void SetDamage(EnemyDamage enemy)
    {
        MainMenu.onNewGame += NewGame;

        enemy.hp = hp;
        enemy.enemyDamage = enemyDamage;

        enemy.healthBar.value = hp;
        enemy.healthBar.maxValue = hp;
    }

    public void SetBehavior(EnemyBehavior enemy)
    {
        enemy.viewAngle = viewAngle;
        enemy.viewDistance = viewDistance;
    }

    public void LevelUp(EnemyDamage enemy)
    {
        hp = enemy.hp;
    }

    public void NewGame()
    {
        hp = baseHP;
        enemyDamage = baseDamage;
    }
}
