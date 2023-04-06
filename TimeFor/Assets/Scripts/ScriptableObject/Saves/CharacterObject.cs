using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Object/ItemObject/Character")]
public class CharacterObject : ItemObject
{
    [Header("�����")]

    [Header("�������")]
    public int levelId;

    [Header("����������")]
    public int health;
    public float stamina;
    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("���������������")]
    public Vector3 position;

}
