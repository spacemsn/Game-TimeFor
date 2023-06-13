using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("����� ����������")]
    public string fileName;
    public string dateSave;

    [Header("�������������� ������")]
    [Header("�����")]
    public string playerName;
    [Header("������� �����")]
    public int LevelScene;
    [Header("������� ������")]
    public int levelPlayer;
    [Header("������� ����")]
    public int experience;
    [Header("��������� ����")]
    public int experienceRequired;
    [Header("��������")]
    public float health;
    [Header("������������ ��������")]
    public float healthMax;
    [Header("�������������")]
    public float stamina;
    [Header("������������ ������������")]
    public float staminaMax;
    [Header("������� ����")]
    public float damageBase;
    [Header("������� �� �����")]
    public float damagePercent;
    [Header("�������������� ������")]
    [Header("�������� ������")]
    public float moveSpeed;
    [Header("�������� ����")]
    public float runSpeed;
    [Header("���� ������")]
    public float jumpForce;
    [Header("����/�����")]
    public float debuff;
    [Header("����� ��������")]
    public float smoothTime;
    [Header("���� ��������")]
    private float smoothVelocity;
    [Header("����������� ���������")]
    public bool isManagement;
    [Header("����� �� �������")]
    private bool isGrounded = true;
    [Header("��������� ��������������")]
    public Vector3 position;
    [Header("�������")]
    public Quaternion rotation;

    [Header("���������")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    public PlayerData(SaveData character)
    {
        dateSave = System.DateTime.Now.ToString();
        playerName = System.Environment.MachineName;
        LevelScene = character.LevelScene;
        levelPlayer = character.levelPlayer;
        experience = character.experience;
        experienceRequired = character.experienceRequired;
        health = character.health;
        healthMax = character.healthMax;
        stamina = character.stamina;
        staminaMax = character.staminaMax;
        damageBase = character.damageBase;
        damagePercent = character.damagePercent;
        moveSpeed = character.moveSpeed;
        runSpeed = character.runSpeed;
        jumpForce = character.jumpForce;
        debuff = character.debuff;
        position = character.position;
        rotation = character.rotation;

        slots = new List<InventorySlot>(character.slots);
    }

    public void LoadSave(SaveData character)
    {
        character.playerName = playerName;
        character.LevelScene = LevelScene;
        character.levelPlayer = levelPlayer;
        character.experience = experience;
        character.experienceRequired = experienceRequired;
        character.health = health;
        character.healthMax = healthMax;
        character.stamina = stamina;
        character.staminaMax = staminaMax;
        character.damageBase = damageBase;
        character.damagePercent = damagePercent;
        character.moveSpeed = moveSpeed;
        character.runSpeed = runSpeed;
        character.jumpForce = jumpForce;
        character.debuff = debuff;
        character.position = position;
        character.rotation = rotation;
    }
}

[CreateAssetMenu(fileName = "Save player", menuName = "Save")]
public class SaveData : ItemObject
{
    [Header("����� ����������")]
    public string dateSave;
    [Header("�������������� ������")]
    [Header("�����")]
    public string playerName = "����� �����";
    [Header("������� �����")]
    public int LevelScene;
    [Header("������� ������")]
    public int levelPlayer;
    [Header("������� ����")]
    public int experience;
    [Header("��������� ����")]
    public int experienceRequired;
    [Header("��������")]
    public float health;
    [Header("������������ ��������")]
    public float healthMax;
    [Header("�������������")]
    public float stamina;
    [Header("������������ ������������")]
    public float staminaMax;
    [Header("������� ����")]
    public float damageBase;
    [Header("������� �� �����")]
    public float damagePercent;
    [Header("�������������� ������")]
    [Header("�������� ������")]
    public float moveSpeed;
    [Header("�������� ����")]
    public float runSpeed;
    [Header("���� ������")]
    public float jumpForce;
    [Header("����/�����")]
    public float debuff;
    [Header("����� ��������")]
    public float smoothTime;
    [Header("���� ��������")]
    private float smoothVelocity;
    [Header("����������� ���������")]
    public bool isManagement;
    [Header("����� �� �������")]
    private bool isGrounded = true;
    [Header("��������� ��������������")]
    public Vector3 position;
    [Header("�������")]
    public Quaternion rotation;

    [Header("���������")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    // ��������� ������ ����������� �������
    PlayerData playerData;
    public List<PlayerData> savedData;

    public void SetSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        indicators.GetSceneIndex();
        dateSave = System.DateTime.Now.ToString();
        playerName = System.Environment.MachineName;
        LevelScene = indicators.LevelScene;
        levelPlayer = indicators.levelPlayer;
        experience = indicators.experience;
        experienceRequired = indicators.experienceRequired;
        health = indicators.Health;
        healthMax = indicators.healthMax;
        stamina = indicators.Stamina;
        staminaMax = indicators.staminaMax;
        damageBase = indicators.damageBase;
        damagePercent = indicators.damagePercent;
        moveSpeed = move.moveSpeed;
        runSpeed = move.runSpeed;
        jumpForce = move.jumpForce;
        debuff = move.debuff;
        position = move.gameObject.transform.position;
        rotation = move.gameObject.transform.rotation;

        character.book.SaveInventory();

        playerData = new PlayerData(this);
        savedData.Add(playerData);
    }

    public void LoadSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        savedData[savedData.Count - 1].LoadSave(this);

        indicators.playerName = playerName;
        indicators.LevelScene = LevelScene;
        indicators.levelPlayer = levelPlayer;
        indicators.experience = experience;
        indicators.experienceRequired = experienceRequired;
        indicators.health = health;
        indicators.healthMax = healthMax;
        indicators.stamina = stamina;
        indicators.staminaMax = staminaMax;
        indicators.damageBase = damageBase;
        indicators.damagePercent = damagePercent;
        move.moveSpeed = moveSpeed;
        move.runSpeed = runSpeed;
        move.jumpForce = jumpForce;
        move.debuff = debuff;
        move.gameObject.transform.position = position;
        move.gameObject.transform.rotation = rotation;

        character.book.SaveInventory();
    }
}
