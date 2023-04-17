using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GloballSetting : MonoCache
{
    [Header("������")]
    [SerializeField] public PauseScript pauseScript;
    [SerializeField] public InventoryScript inventoryScript;
    [SerializeField] public DeathScript deathScript;
    [SerializeField] public ScrollScript scrollScript;

    [Header("UI")]
    public GameObject InventoryPanel;
    public GameObject IndecatorPanel;
    public GameObject DeathPanel;
    public GameObject PausePanel;

    [Header("�������")]
    public GameObject character;

    [Header("���������� ��������")]
    [SerializeField] bool isVisible = true;
    [SerializeField] public CinemachineFreeLook freeLook;

    private void Start()
    {
        #region Find Component

        character = GameObject.FindGameObjectWithTag("Player");
        freeLook = GameObject.FindGameObjectWithTag("FreeLook").GetComponent<CinemachineFreeLook>();
        inventoryScript = character.GetComponent<InventoryScript>();
        pauseScript = GetComponent<PauseScript>();
        deathScript = GetComponent<DeathScript>();
        scrollScript = GetComponent<ScrollScript>();

        #endregion

        #region Find UI

        InventoryPanel = GameObject.Find("InventoryPanel");
        IndecatorPanel = GameObject.Find("IndecatorsPanel");
        DeathPanel = GameObject.Find("DeathPanel");
        PausePanel = GameObject.Find("PausePanel");

        #endregion 

        notVisible();

        scrollScript.SetComponent(freeLook);
        deathScript.SetComponent(DeathPanel);
        pauseScript.SetComponent(PausePanel);

    }

    public override void OnTick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScript.isOpenPanel == false)
        {
            pauseScript.OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && pauseScript.isOpenPanel == false)
        {
            inventoryScript.OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Visible();
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            notVisible();
        }
    }

    public void Visible()
    {
        if (isVisible == false && freeLook != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isVisible = true;
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
            freeLook.m_XAxis.m_InputAxisValue = 0;
        }
    }

    public void notVisible()
    {
        if (isVisible && freeLook != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isVisible = false;
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }

    private void OnEnterButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) { Visible(); }
        else if (Input.GetKeyUp(KeyCode.LeftAlt)) { notVisible(); }
    }
}
