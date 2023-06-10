using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    private BoxCollider portal;

    private void Start()
    {
        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted += Open;

        portal = gameObject.GetComponent<BoxCollider>(); 
        portal.enabled = false;
    }

    private void Open(Quest quest)
    {
        portal.enabled = true;

        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted -= Open;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
