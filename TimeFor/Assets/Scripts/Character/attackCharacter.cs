using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class attackCharacter : MonoCache
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("��������������")]
    private bool attacking;
    [SerializeField] private float maxDistance;
    private Transform rightHand;

    [Header("�����")]
    [SerializeField] Collider[] enemies;
    [SerializeField] LayerMask enemyLayer;

    [Header("���������")]
    [SerializeField] public moveCharacter move;
    [SerializeField] public indicatorCharacter indicator;
    [SerializeField] public Animator animator;

    [Header("UI")]
    [SerializeField] public Transform QuickslotPanel;

    [SerializeField] public Sprite FireSelectSprite;
    [SerializeField] public Sprite WaterSelectSprite;
    [SerializeField] public Sprite AirSelectSprite;
    [SerializeField] public Sprite TerraSelectSprite;

    [SerializeField] public Sprite selectedSprite;
    [SerializeField] public Sprite notSelectedSprite;

    [Header("����")]
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip waterSound;
    public AudioClip airSound;
    public AudioClip terraSound;

    [Header("����� ����")]
    [SerializeField] public SkillSlot WaterSlot;
    [SerializeField] public SkillSlot FireSlot;
    [SerializeField] public SkillSlot AirSlot;
    [SerializeField] public SkillSlot TerraSlot;

    [Header("���� ����")]
    [SerializeField] private skillItem WaterAttack;
    [SerializeField] private skillItem FireAttack;
    [SerializeField] private skillItem AirAttack;
    [SerializeField] private skillItem TerraAttack;
    private Collider currentEnemy;
    private skillItem currentAttack;
    private GameObject currentSpell;

    public float currentDamage;
    public float currentPercent;

    [SerializeField] public int currentQuickslotID = -1;
    [SerializeField] public int oldQuickslotID;

    [Header("������")]
    [SerializeField] KeyCode KeyCode1;
    [SerializeField] KeyCode KeyCode2;
    [SerializeField] KeyCode KeyCode3;
    [SerializeField] KeyCode KeyCode4;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
        indicator = GetComponent<indicatorCharacter>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        rightHand = GameObject.Find("ArmSmall").transform;
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        WaterSlot = uI.WaterSlot;
        FireSlot = uI.FireSlot;
        AirSlot = uI.AirSlot;
        TerraSlot = uI.TerraSlot;
        QuickslotPanel = uI.QuickslotPanel;
            
        WaterSlot.SetIcon(WaterAttack.icon);
        FireSlot.SetIcon(FireAttack.icon);
        AirSlot.SetIcon(AirAttack.icon);
        TerraSlot.SetIcon(TerraAttack.icon);
    }

    private void Update()
    {
        // ���������� �����
        for (int i = 0; i < QuickslotPanel.childCount; i++)
        {
            // ���� �� �������� �� ������� 1 �� 5 ��...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // ��������� ���� ��� ��������� ���� ����� ����� ������� � ��� ��� ������, ��
                if (currentQuickslotID == i)
                {
                    // ������ �������� "selected" �� ���� ���� �� "not selected" ��� ��������
                    if (QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    }
                    //else
                    //{
                    //    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    //}
                }
                // ����� �� ������� �������� � ����������� ����� � ������ ���� ������� �� ��������
                else
                {
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    oldQuickslotID = currentQuickslotID;

                    currentQuickslotID = i;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        switch (currentQuickslotID)
        {
            case 0:
                {
                    currentAttack = WaterAttack; audioSource.clip = fireSound; currentDamage = indicator.damageBaseFire; currentPercent = indicator.damagePercentFire;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = FireSelectSprite;
                    break;
                }

            case 1:
                { 
                    currentAttack = FireAttack; audioSource.clip = waterSound; currentDamage = indicator.damageBaseWater; currentPercent = indicator.damagePercentWater;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = WaterSelectSprite;
                    break;
                }

            case 2:
                {
                    currentAttack = AirAttack; audioSource.clip = airSound; currentDamage = indicator.damageBaseAir; currentPercent = indicator.damagePercentAir;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = AirSelectSprite; break;
                }

            case 3:
                {
                    currentAttack = TerraAttack; audioSource.clip = terraSound; currentDamage = indicator.damageBaseTerra; currentPercent = indicator.damagePercentTerra;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = TerraSelectSprite; break;
                }

            default:
                {
                    currentQuickslotID = oldQuickslotID;
                    currentAttack = WaterAttack; currentDamage = indicator.damageBaseFire; currentPercent = indicator.damagePercentFire;
                    QuickslotPanel.GetChild(currentQuickslotID).GetComponent<Image>().sprite = WaterSelectSprite; break;
                }
        }

        FindEnemies();
        Shoot();
    }

    public void FindEnemies()
    {
        // ����� ���������� �����
        enemies = Physics.OverlapSphere(transform.position, maxDistance, enemyLayer);
        if (enemies.Length > 0)
        {
            currentEnemy = enemies[0];
            float closestDistance = Vector3.Distance(transform.position, currentEnemy.transform.position);
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    currentEnemy = enemy;
                    closestDistance = distance;
                }
            }
        }
    }

    private void Shoot()
    {
        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentEnemy != null)
            {
                move.isManagement = false; audioSource.pitch = 1.25f; audioSource.Play();
                transform.LookAt(currentEnemy.transform.position, Vector3.up);
                animator.SetBool("Attack1", true);
            }

            Invoke("ResetAttack", currentAttack.attackRollback);
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }

    public void StartAnimation()
    {
        // ������� ���������������� ������� � ���������� �����
        GameObject centerOfEnemy = currentEnemy.GetComponent<EnemyBehavior>().centerOfEnemy.gameObject;
        currentSpell = Instantiate(currentAttack.itemPrefab, rightHand.position, rightHand.transform.rotation);
        currentSpell.GetComponent<FireBall>().SetTarget(centerOfEnemy.transform);

        // ������ ������� ��� ��������� �����
        attacking = true;
    }   
    
    public void EndAnimation()
    {
        move.isManagement = true;
        animator.SetBool("Attack1", false);
    }
}
