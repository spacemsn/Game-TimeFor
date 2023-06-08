using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string questDescription;

    public enum QuestType
    {
        KillEnemies,
        CollectItems
    }

    public QuestType questType;

    // ��� ������� "����� ������"
    public List<GameObject> enemiesToKill;
    public int enemiesToKillCount;

    // ��� ������� "������� ��������"
    public List<GameObject> itemsToCollect;
    public int itemsToCollectCount;

    // ������������ ��������� ������ �� ���������� �������
    public int progressQuestCount;

    [Header("������� �� ���������� �������")]
    public int experiencePoints;
    public GameObject rewardItem;
}
