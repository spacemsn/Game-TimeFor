using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Object/ItemObject/Character")]
public class CharacterObject : ItemObject
{
    [Header("�����")]
    public string playerName;

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

    public void Save(CharacterStatus character)
    {
        playerName = character.playerName;
        levelId = character.levelId;
        health = character.health;
        stamina = character.stamina;
        moveSpeed = character.moveSpeed;
        runSpeed = character.runSpeed;
        jumpForce = character.jumpForce;
        debuff = character.debuff;
        position = character.position;
        objectPrefab = character.gameObject;
    }

    public void Load(CharacterStatus character)
    {
        character.playerName = playerName;
        character.levelId = levelId;
        character.health = health;
        character.stamina = stamina;
        character.moveSpeed = moveSpeed;
        character.runSpeed = runSpeed;
        character.jumpForce = jumpForce;
        character.debuff = debuff;
        character.position = position;
    }
}
