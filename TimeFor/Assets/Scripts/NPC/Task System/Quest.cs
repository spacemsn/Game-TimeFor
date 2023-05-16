using UnityEngine;

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
    public GameObject[] enemiesToKill;
    public int enemiesToKillCount;

    // ��� ������� "������� ��������"
    public GameObject[] itemsToCollect;
    public int itemsToCollectCount;
}
