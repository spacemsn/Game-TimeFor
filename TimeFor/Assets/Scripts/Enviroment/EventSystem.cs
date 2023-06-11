using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private BoxCollider portal;
    [SerializeField] private Quest needQuest;

    private void Start()
    {
        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted += Open;

        portal = gameObject.GetComponent<BoxCollider>();
        portal.isTrigger = false;
    }

    private void OnDestroy()
    {
        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted -= Open;
    }

    private void Open(Quest quest)
    {
        if (quest == needQuest)
        {
            Debug.Log("����� ����������");
            portal.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
