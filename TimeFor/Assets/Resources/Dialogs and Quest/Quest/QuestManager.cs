using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{ 
    [Header("�������")]
    public Quest currentQuest;

    public static Action<Quest> onQuestCompleted;
    public bool isQuestCompleted = false;

    // ��� ������� "����� ������"
    public List<GameObject> enemiesToKill;
    public int enemiesToKillCount;

    // ��� ������� "������� ��������"
    public List<GameObject> itemsToCollect;
    public int itemsToCollectCount;

    public int progressQuestCount;

    public void GetQuest(Quest quest)
    {
        this.currentQuest = quest;

        switch (currentQuest.questType)
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

        progressQuestCount = currentQuest.progressQuestCount;
    }

    public void ProgressQuest()
    {
        if(!isQuestCompleted && currentQuest != null)
        {
            progressQuestCount++;
            if (currentQuest.questType == Quest.QuestType.KillEnemies && progressQuestCount >= enemiesToKillCount)
            {
                CompleteQuest();
            }
            else if (currentQuest.questType == Quest.QuestType.CollectItems && progressQuestCount >= itemsToCollectCount)
            {
                CompleteQuest();
            }
        }

    }

    public void KillEnemy(GameObject currentEnemy)
    {
        foreach(GameObject enemy in enemiesToKill)
        {
            if(enemy == currentEnemy)
            {
                ProgressQuest();
            }
        }
    }

    public void CollectItems(GameObject currentItem)
    {
        foreach (GameObject item in itemsToCollect)
        {
            if (item == currentItem)
            {
                ProgressQuest();
            }
        }
    }

    public void CompleteQuest()
    {
        // ��������� �������� ��� ���������� �������
        isQuestCompleted = true;
        onQuestCompleted.Invoke(currentQuest);

        //ClearQuest();
    }

    public void ClearQuest()
    {
        isQuestCompleted = false;
        currentQuest = null;

        enemiesToKillCount = 0;
        itemsToCollectCount = 0;
        progressQuestCount = 0;
    }
}
