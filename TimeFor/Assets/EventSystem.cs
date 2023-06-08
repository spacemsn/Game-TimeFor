using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    private Collider portal;

    private void Start()
    {
        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted += Open;

        portal.enabled = false;
    }

    private void Open(Quest quest)
    {
        Debug.Log("����� ���������");
        portal.enabled = true;

        // ������������� �� ������� OnQuestCompleted
        QuestManager.onQuestCompleted -= Open;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            DontDestroyOnLoad(other.gameObject);
        }
    }
}
