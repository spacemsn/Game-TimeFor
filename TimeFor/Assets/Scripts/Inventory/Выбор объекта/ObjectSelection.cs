//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ObjectSelection : MonoBehaviour
//{
//    public GameObject selectionMenu; // ������ �� ���� ������ �������
//    public Text menuTitle; // ������ �� ����� ��������� ����
//    public Button menuOptionPrefab; // ������ �� ������ ����� ����
//    public Transform menuOptionsPanel; // ������ �� ������ ����� ����

//    private RaycastHit[] hits;
//    [SerializeField] private List<GameObject> selectableObjects;
//    [SerializeField] private GameObject selectedObject;
//    private bool isMenuOpen;

//    public int selectedIndex;

//    void Start()
//    {
//        // ���� ��� ������� � ����� "Selectable"
//        selectableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));

//        // ������� ������ ��� ������� ����������� �������
//        foreach (GameObject obj in selectableObjects)
//        {
//            //SelectObject()


//            Button button = Instantiate(menuOptionPrefab, menuOptionsPanel);
//            button.GetComponentInChildren<Text>().text = obj.name;
//            button.onClick.AddListener(delegate { SelectObject(obj); });
//        }

//        // �������� ���� ������ �������
//        selectionMenu.SetActive(false);
//        isMenuOpen = false;
//    }

//    void Update()
//    {
//        // ���������, ���� �� ������� �� ������ ���� ������ �������
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            if (!isMenuOpen)
//            {
//                // ���������� ���� ������ �������
//                selectionMenu.SetActive(true);
//                isMenuOpen = true;
//            }
//            else
//            {
//                // �������� ���� ������ �������
//                selectionMenu.SetActive(false);
//                isMenuOpen = false;
//            }
//        }

//        // ���������, ���� �� ������� ����� ������ ����
//        if (Input.GetMouseButtonDown(0))
//        {
//            // ���������, ����� �� ��� �� �����-���� ������
//            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
//            foreach (RaycastHit hit in hits)
//            {
//                if (selectableObjects.Contains(hit.transform.gameObject))
//                {
//                    SelectObject(hit.transform.gameObject);
//                    break;
//                }
//            }
//        }

//        // ������������ ��������� �������� ����
//        if (Input.GetKeyDown(KeyCode.UpArrow)) //(Input.GetAxis("Mouse ScrollWheel") > 0f)
//        {
//            // �������� ������ ��������� ������ ����
//            selectedIndex = selectableObjects.IndexOf(selectedObject);

//            // ���� ������� ��������� ������, �������� ������
//            if (selectedIndex >= selectableObjects.Count - 1)
//            {
//                selectedIndex = 0;
//            }
//            else
//            {
//                // �������� ��������� ������
//                selectedIndex++;
//            }

//            // �������� ������, ��������� � ��������� �������
//            SelectObject(selectableObjects[selectedIndex]);
//        }
//        else if (Input.GetKeyDown(KeyCode.DownArrow)) //(Input.GetAxis("Mouse ScrollWheel") < 0f)
//        {
//            // �������� ������ ��������� ������ ����
//            selectedIndex = selectableObjects.IndexOf(selectedObject);

//            // ���� ������� ������ ������, �������� ���������
//            if (selectedIndex <= 0)
//            {
//                selectedIndex = selectableObjects.Count - 1;
//            }
//            else
//            {
//                // �������� ���������� ������
//                selectedIndex--;
//            }

//            // �������� ������, ��������� � ��������� �������
//            SelectObject(selectableObjects[selectedIndex]);
//        }

//        // ���������, ���� �� ������ ������ F
//        if (Input.GetKeyDown(KeyCode.F) && selectedObject != null)
//        {
//            // �������� ��������� ������ ����
//            selectedObject.GetComponent<Button>().onClick.Invoke();
//        }
//    }

//    void SelectObject(GameObject obj)
//    {
//        // ���������� ��������� ���� ��������
//        foreach (GameObject selectableObj in selectableObjects)
//        {
//            // ������ ���� �� �������
//            selectableObj.transform.GetComponent<Image>().color = Color.white;
//        }

//        // �������� ����� ������
//        selectedObject = obj;

//        // �������� ��������� ������
//        selectedObject.GetComponent<Image>().color = Color.green;

//        // ��������� ��������� ����
//        menuTitle.text = selectedObject.name;

//        // �������� ���� ������ �������
//        //selectionMenu.SetActive(false);
//        //isMenuOpen = false;
//    }

//}

////public class ObjectSelectionBase : MonoBehaviour
////{
////    public GameObject selectionMenu; // ������ �� ���� ������ �������
////    public Text menuTitle; // ������ �� ����� ��������� ����
////    public Button menuOptionPrefab; // ������ �� ������ ����� ����
////    public Transform menuOptionsPanel; // ������ �� ������ ����� ����

////    private RaycastHit[] hits;
////    private List<GameObject> selectableObjects;
////    private GameObject selectedObject;
////    private bool isMenuOpen;

////    void Start()
////    {
////        ���� ��� ������� � ����� "Selectable"
////        selectableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));

////        ������� ������ ��� ������� ����������� �������
////        foreach (GameObject obj in selectableObjects)
////        {
////            Button button = Instantiate(menuOptionPrefab, menuOptionsPanel);
////            button.GetComponentInChildren<Text>().text = obj.name;
////            button.onClick.AddListener(delegate { SelectObject(obj); });
////        }

////        �������� ���� ������ �������
////        selectionMenu.SetActive(false);
////        isMenuOpen = false;
////    }

////    void Update()
////    {
////        ���������, ���� �� ������� �� ������ ���� ������ �������
////        if (Input.GetKeyDown(KeyCode.H))
////        {
////            if (!isMenuOpen)
////            {
////                ���������� ���� ������ �������
////                selectionMenu.SetActive(true);
////                isMenuOpen = true;
////            }
////            else
////            {
////                �������� ���� ������ �������
////                selectionMenu.SetActive(false);
////                isMenuOpen = false;
////            }
////        }

////        ���������, ���� �� ������� ����� ������ ����
////        if (Input.GetMouseButtonDown(0))
////        {
////            ���������, ����� �� ��� �� ����� - ���� ������
////           hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
////            foreach (RaycastHit hit in hits)
////            {
////                if (selectableObjects.Contains(hit.transform.gameObject))
////                {
////                    SelectObject(hit.transform.gameObject);
////                    break;
////                }
////            }
////        }
////    }

////    ������� ������ �������
////    public void SelectObject(GameObject obj)
////    {
////        �������� ������
////        selectedObject = obj;

////        ��������� ���� ������ �������
////        selectionMenu.SetActive(false);
////        isMenuOpen = false;

////        ��������� ����� ��������� ����
////        menuTitle.text = "Selected: " + selectedObject.name;

////        ��������� �������� � ��������� ��������...
////    }
////}
