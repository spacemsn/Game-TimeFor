using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public class QuestReward : MonoBehaviour
{
    public static Action<int> onQuestReward;
    public static Action onAfterDialog;

    private NPCBehaviour npcBehaviour;

    [Header("�������")]
    public Quest quest;

    [Header("������� �� ���������� �������")]
    public int experiencePoints = 100;
    public GameObject rewardItem;

    private void Start()
    {
        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted += GiveReward;

        npcBehaviour = gameObject.GetComponent<NPCBehaviour>();
    }

    private void OnDestroy()
    {
        // ������������ �� ������� OnQuestCompleted ��� ����������� �������
        QuestManager.onQuestCompleted -= GiveReward;
    }

   
    private void GiveReward(Quest quest)
    {
        // ���� ������ ���� �����
        //other.GetComponent<Player>().AddExperience(experiencePoints);

        // ���� ������ ����� �������
        if (rewardItem != null)
        {
            // ��������� �������� � ��������� ��� ������� �������� 
        }

        // ��������� ������� ����� ���������� �������
        //GameManager.instance.CompleteQuest();

        this.quest = quest;
        experiencePoints = quest.experiencePoints;
        rewardItem = quest.rewardItem;

        if(quest.answer != null)
        {
            DialogAfterQuest();
        }

        onQuestReward.Invoke(100);
        onAfterDialog.Invoke();
    }

    private void DialogAfterQuest()
    {
        npcBehaviour.startDialog = quest.answer;
    }
}
