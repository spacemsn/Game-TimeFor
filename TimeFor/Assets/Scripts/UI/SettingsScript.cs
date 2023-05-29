using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Controlls
{
    [Header("Mouse")]
    public float SensitivityY;
    public float SensitivityX;

    public Controlls(float sensitivityY, float sensitivityX)
    {
        this.SensitivityY = sensitivityY;
        this.SensitivityX = sensitivityX;
    }
}

public class SettingsScript : MonoBehaviour
{
    public EntryPoint entryPoint;

    [Header("����������")]
    [SerializeField] private GloballSetting settings;
    [SerializeField] private PauseScript pauseScript;
    [SerializeField] private CinemachineFreeLook freeLook;

    [Header("���������")]
    [SerializeField] private SettingsObject Default;
    [SerializeField] private SettingsObject Settings;
    [SerializeField] private GameObject SettingsPanel;
    public bool isOpenPanel = true;

    [Header("UI")]
    [SerializeField] private Slider SensitivityYSlider;
    [SerializeField] private Slider SensitivityXSlider;

    private void Start()
    {
        #region Get components

        settings = GetComponent<GloballSetting>(); entryPoint = settings.entryPoint; SettingsPanel = entryPoint.UI.SettingPanel.gameObject;
        pauseScript = GetComponent<PauseScript>();

        if (entryPoint)
        {
            freeLook = entryPoint.UI.freeLook;
            freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;

            SensitivityYSlider = entryPoint.UI.SensitivityYSlider;
            SensitivityXSlider = entryPoint.UI.SensitivityXSlider;

            SensitivityYSlider.value = Settings.SensitivityY;
            SensitivityXSlider.value = Settings.SensitivityX;
        }

        #endregion
    }

    public void SaveSettings()
    {
        Settings.SensitivityY = SensitivityYSlider.value;
        Settings.SensitivityX = SensitivityXSlider.value;

        if (freeLook != null)
        {
            freeLook.m_YAxis.m_MaxSpeed = Settings.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Settings.SensitivityX;
        }
    }

    public void SetDefault()
    {
        if (freeLook != null)
        {
            freeLook.m_YAxis.m_MaxSpeed = Default.SensitivityY;
            freeLook.m_XAxis.m_MaxSpeed = Default.SensitivityX;
        }

        SensitivityYSlider.value = Default.SensitivityY;
        SensitivityXSlider.value = Default.SensitivityX;
    }    

    public void OpenMenu()
    {
        if (isOpenPanel == false)
        {
            SettingsPanel.gameObject.SetActive(true);
            isOpenPanel = true;
        }
        else if (isOpenPanel == true)
        {
            SettingsPanel.gameObject.SetActive(false);
            isOpenPanel = false;
        }
    }

    public void Back()
    {
        pauseScript.OpenMenu();
        OpenMenu();
    }

}
