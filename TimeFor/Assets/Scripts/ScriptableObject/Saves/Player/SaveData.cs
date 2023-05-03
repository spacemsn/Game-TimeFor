using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save/Json")]
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
    public float health;
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

    public void SetSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        levelId = character.levelId;
        levelPlayer = indicators.lvlPlayer;
        health = indicators.Health;
        stamina = indicators.Stamina;
        damageBase = character.damageBase;
        damagePercent = character.damagePercent;
        moveSpeed = move.moveSpeed;
        runSpeed = move.runSpeed;
        jumpForce = move.jumpForce;
        debuff = move.debuff;
        position = character.position;

        playerData = new PlayerData(this);
        savedPlayers.Add(playerData);
    }

    public void LoadSave(mainCharacter character, indicatorCharacter indicators, moveCharacter move)
    {
        savedPlayers[saveIndex].LoadSave(this);

        indicators.lvlPlayer = levelPlayer;
        indicators.Health = health;
        indicators.Stamina = stamina;
        character.damageBase = damageBase;
        character.damagePercent = damagePercent;
        move.moveSpeed = moveSpeed;
        move.runSpeed = runSpeed;
        move.jumpForce = jumpForce;
        move.debuff = debuff;
        character.transform.position = position;

    }
}
