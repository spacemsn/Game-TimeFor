using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save/Json")]
[SerializeField]
public class SaveData : ItemObject
{
    [Header("����� ����������")]
    public string dateSave;

    [Header("�����")]
    public string playerName;

    [Header("�������")]
    public int levelId;

    [Header("����������")]
    public int levelPlayer;
    public int health;
    public float stamina;
    public float damageBase;
    public float damagePercent;

    public float moveSpeed;
    public float runSpeed;
    public float jumpForce;
    public float debuff;

    [Header("���������������")]
    public Vector3 position;
    public Vector3 origPosition;

    [Header("���������")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    // ��������� ������ ����������� �������
    PlayerData playerData;
    public int saveIndex = 0;
    public List<PlayerData> savedPlayers;

    private void Awake()
    {
        objectPrefab = Resources.Load<GameObject>("Prefabs/Player/Character");
    }

    public void SetSave(CharacterStatus character)
    {
        levelId = character.levelId;
        levelPlayer = character.levelPlayer;
        health = character.health;
        stamina = character.stamina;
        damageBase = character.damageBase;
        damagePercent = character.damagePercent;
        moveSpeed = character.moveSpeed;
        runSpeed = character.runSpeed;
        jumpForce = character.jumpForce;
        debuff = character.debuff;
        position = character.position;

        playerData = new PlayerData(this);
        savedPlayers.Add(playerData);
    }

    public void LoadSave(CharacterStatus character)
    {
        savedPlayers[saveIndex].LoadSave(this);

        character.levelPlayer = levelPlayer;
        character.health = health;
        character.stamina = stamina;
        character.damageBase = damageBase;
        character.damagePercent = damagePercent;
        character.moveSpeed = moveSpeed;
        character.runSpeed = runSpeed;
        character.jumpForce = jumpForce;
        character.debuff = debuff;
        character.transform.position = position;
    }
}
