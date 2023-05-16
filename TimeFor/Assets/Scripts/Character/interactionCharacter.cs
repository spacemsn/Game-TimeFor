using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
{
    [Header("������")]
    [SerializeField] private Image imageE;
    [SerializeField] private Image imageT;

    [Header("������ ��������������")]
    [SerializeField] private float radius;
    [SerializeField] private bool isGizmos = true;

    [Header("��������� ��������������")]
    [SerializeField] private float maxDistance;
    private Ray ray;
    private RaycastHit hit;

    [Header("����������")]
    public GameObject GlobalSettings;
    private moveCharacter move;
    public Collider[] Interactions;
    public Collider[] NPC;

    public LayerMask maskNPC;
    public LayerMask maskInteractions;

    [Header("������")]
    private DialogManager dialog;

    public List<Button> selectButtons;
    public Button oldButton;
    public Button currentButton;
    public Transform buttonParent;
    public int selectedIndex;

    public KeyCode keyOne;
    public KeyCode keyTwo;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
        dialog = GetComponent<DialogManager>();
    }

    private void Ray()
    {
        ray = new Ray(transform.position + new Vector3(0, 1f, 0), transform.forward);
    }

    private void DrawRay()
    {
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        }

        if (hit.transform == null)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    private void Interact() // ����� �������������� � ���������� 
    {
        if (hit.transform != null && hit.transform.GetComponent<Interactions>())
        {
            //imageE.enabled = true;
            //imageT.enabled = true;

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }

    private void Radius() // ����� �������������� � ��������� ����� ������ 
    {
        var Inventory = gameObject.GetComponent<bookCharacter>();

        Interactions = Physics.OverlapSphere(transform.position, radius, maskInteractions);

        for (int i = 0; i < Interactions.Length; i++)
        {
            Rigidbody rigidbodyInteractions = Interactions[i].attachedRigidbody;
            if (rigidbodyInteractions != null)
            {
                imageT.enabled = true;

                if (Input.GetKeyDown(KeyCode.E)) // ����� � ���� �������
                {
                    if (rigidbodyInteractions.GetComponent<Rigidbody>() != null)
                    {
                        //rigidbodyInteractions.GetComponent<Interactions>().PickUp();
                    }
                }
                //if (Input.GetKeyDown(KeyCode.T)) // ����� ������� � ���������
                //{
                //    ItemPrefab item = rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>();
                //    if (rigidbodyInteractions.gameObject.GetComponent<ItemPrefab>() != null)
                //    {
                //        Inventory.AddItem(item.item, item.amount);
                //    }
                //    Destroy(rigidbodyInteractions.gameObject);
                //}
            }
            else { imageT.enabled = false; }
        }

        NPC = Physics.OverlapSphere(transform.position, radius, maskNPC);

        for (int i = 0; i < NPC.Length; i++)
        {
            Rigidbody rigidbodyInteractions = NPC[i].attachedRigidbody;
            if (rigidbodyInteractions != null)
            {
                imageE.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (NPC[i].GetComponent<NPCBehaviour>())
                    {
                        NPCBehaviour NPCbehaviour = NPC[i].GetComponent<NPCBehaviour>();
                        transform.LookAt(NPCbehaviour.transform, new Vector3(0, transform.position.y, 0));
                        dialog.StartDialog(NPCbehaviour);
                    }
                }
            }
            else { imageE.enabled = false; }
        }
    }

    private void OnDrawGizmos()
    {
        if (isGizmos == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 1f, 0), radius);
        }
    }

    public override void OnUpdate()
    {
        Ray();
        DrawRay();
        Interact();
        Radius();

        selectButtons = buttonParent.GetComponentsInChildren<Button>().ToList();


        if (Input.GetKeyDown(keyOne))
        {
            // �������� ������ ��������� ������ ����
            selectedIndex = selectButtons.IndexOf(currentButton);

            // ���� ������� ��������� ������, �������� ������
            if (selectedIndex >= selectButtons.Count - 1)
            {
                selectedIndex = 0;
            }
            else
            {
                // �������� ��������� ������
                selectedIndex++;
            }

            // �������� ������, ��������� � ��������� �������
            SelectObject(selectButtons[selectedIndex]);
        }
        else if (Input.GetKeyDown(keyTwo))
        {
            // �������� ������ ��������� ������ ����
            selectedIndex = selectButtons.IndexOf(currentButton);

            // ���� ������� ������ ������, �������� ���������
            if (selectedIndex <= 0)
            {
                selectedIndex = selectButtons.Count - 1;
            }
            else
            {
                // �������� ���������� ������
                selectedIndex--;
            }

            // �������� ������, ��������� � ��������� �������
            SelectObject(selectButtons[selectedIndex]);
        }

    }

    void SelectObject(Button obj)
    {
        // ���������� ��������� ���� ��������
        foreach (Button selectableObj in selectButtons)
        {
            // ������ ���� �� �������
            selectableObj.transform.GetComponent<Image>().color = Color.white;
        }

        // �������� ����� ������
        if(currentButton != null) { oldButton = currentButton; oldButton.GetComponent<SelectObjectButton>().isSelect(); }
        currentButton = obj; currentButton.GetComponent<SelectObjectButton>().isSelect();

        // �������� ��������� ������
        currentButton.GetComponent<Image>().color = Color.green;

    }
}
