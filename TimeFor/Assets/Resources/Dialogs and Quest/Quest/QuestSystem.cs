using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [Header("�������")]
    public Quest currentQuest;
    public bool isDone = false;

    // ��� ������� "����� ������"
    public List<GameObject> enemiesToKill;
    public int enemiesToKillCount;

    // ��� ������� "������� ��������"
    public List<GameObject> itemsToCollect;
    public int itemsToCollectCount;

    public int progressQuestCount;

    private void Start()
    {
        switch(currentQuest.questType)
        {
            case Quest.QuestType.KillEnemies:
                enemiesToKill = currentQuest.enemiesToKill;
                enemiesToKillCount = currentQuest.itemsToCollectCount;
                break;

            case Quest.QuestType.CollectItems:
                itemsToCollect = currentQuest.itemsToCollect;
                itemsToCollectCount = currentQuest.itemsToCollectCount;
                break;
        }

        this.progressQuestCount = currentQuest.progressQuestCount;
    }

    private void Update()
    {
        if(currentQuest.questType == Quest.QuestType.KillEnemies && progressQuestCount >= enemiesToKillCount)
        {
            isDone = true;
        }
        else if(currentQuest.questType == Quest.QuestType.CollectItems && progressQuestCount >= itemsToCollectCount)
        {
            isDone = true;
        }
    }

    public void KillEnemy(GameObject currentEnemy)
    {
        foreach(GameObject enemy in enemiesToKill)
        {
            if(enemy == currentEnemy)
            {
                progressQuestCount++;
            }
        }
    }

    public void CollectItems(GameObject currentItem)
    {
        foreach (GameObject item in itemsToCollect)
        {
            if (item == currentItem)
            {
                progressQuestCount++;
            }
        }
    }
}
