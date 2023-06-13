using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("���������� ���������")]
    [SerializeField] private SaveData currentSave;

    [Header("���������")]
    [SerializeField] private SettingsScript settings;

    [Header("������ �����������")]
    public Transform warningPanel;
    public Text warningText;

    EntryPoint entryPoint;

    private void Start()
    {
        entryPoint = GameObject.Find("EntryPoint").GetComponent<EntryPoint>();
        currentSave = entryPoint.player.saveData;

        warningPanel.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        if (currentSave.savedData.Count <= 0) // �������� �� ������� ���������� � ������
        {
            SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
            entryPoint.globallSetting.globall.notVisible();
            Time.timeScale = 1f;

            currentSave.savedData.Clear();
        }
        else
        {
            string warningContext = "� ��� ��� ���� ����������, ���� �� ������� ����� ����, �� ���� ���������� ���������� ���������.";

            warningPanel.gameObject.SetActive(true); warningText.text = warningContext;
        }
    }

    public void ClearAllSaves()
    {
        SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
        entryPoint.globallSetting.globall.notVisible();
        Time.timeScale = 1f;

        currentSave.savedData.Clear();
    }

    public void Continuo()
    {
        if (currentSave.savedData.Count > 0) // �������� �� ������� ���������� � ������
        {
            SceneLoad.SwitchIndexScene(currentSave.savedData[currentSave.savedData.Count - 1].LevelScene);
            entryPoint.globallSetting.globall.notVisible();
            Time.timeScale = 1f;
        }
        else
        {
            string warningContext = "� ��� ��� ����������, ���������� ������� ����� ����";

            warningPanel.gameObject.SetActive(true); warningText.text = warningContext;
        }
    }

    public void ExiteGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settings.OpenMenu();
    }

    public void CloseWarningPanel()
    {
        warningText.text = "";
        warningPanel.gameObject.SetActive(false);
    }
}
