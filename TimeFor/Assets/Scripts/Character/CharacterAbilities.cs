using UnityEngine;

public class CharacterAbilities : MonoCache
{
    [Header("��������������")]
    private float timer = 0;
    private Transform rightHand;
    private bool attacking; // ���� ���������� �����
    [SerializeField] private float maxDistance; // ���������� ���������� ����������

    [Header("�����")]
    [SerializeField] Collider[] enemies;
    [SerializeField] LayerMask enemyLayer;


    [Header("���������")]
    [SerializeField] public CharacterIndicators indicators;
    [SerializeField] public GloballSetting globalSettings;
    [SerializeField] public CharacterStatus status;
    [SerializeField] public Animator animator;

    [Header("UI")]
    [SerializeField] public GameObject InventoryPanel;
    [SerializeField] public GameObject DealthPanel;
    [SerializeField] public GameObject PausePanel;

    [Header("���� ����")]
    [SerializeField] private SkillObject attackOne;
    [SerializeField] private SkillObject attackTwo;
    [SerializeField] private SkillObject attackThree;
    [SerializeField] private SkillObject attackFour;
    private Collider currentEnemy;
    private SkillObject currentAttack;
    private GameObject currentSpell;

    private enum listSpells { Fire, Water, Air, Ground, }
    [SerializeField] private listSpells spells;

    private void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        status = GetComponent<CharacterStatus>();
        animator = GetComponent<Animator>();

        globalSettings = GameObject.Find("Global Settings").GetComponent<GloballSetting>();
        //inventory = GameObject.Find("SkillsPanel").GetComponent<QuickslotInventory>();
        if (globalSettings != null)
        {
            InventoryPanel = globalSettings.InventoryPanel;
            DealthPanel = globalSettings.DeathPanel;
            PausePanel = globalSettings.PausePanel;
        }
        rightHand = GameObject.Find("ArmSmall").transform;

    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        switch (spells)
        {
            case listSpells.Fire:
                {
                    currentAttack = attackOne; Shoot();
                }
                break;

            case listSpells.Water:
                {
                    currentAttack = attackTwo; Shoot();
                }
                break;

            case listSpells.Air:
                {
                    currentAttack = attackThree; Shoot();
                }
                break;

            case listSpells.Ground:
                {
                    currentAttack = attackFour; Shoot();
                }
                break;

            default: { break; }
        }
    }

    private void Shoot()
    {
        if (!attacking && timer > currentAttack.attackRollback)
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

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0f;
                    status.charMenegment = false;
                    transform.LookAt(currentEnemy.transform.position, Vector3.up);
                    animator.SetBool("Attack1", true);
                }

                Invoke("ResetAttack", currentAttack.attackRollback);
            }
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
        currentSpell = Instantiate(attackOne.objectPrefab, rightHand.position, rightHand.transform.rotation);
        currentSpell.GetComponent<FireBall>().SetTarget(centerOfEnemy.transform, currentAttack.speed);

        // ������ ������� ��� ��������� �����
        attacking = true;
    }   
    
    public void EndAnimation()
    {
        status.charMenegment = true;
        animator.SetBool("Attack1", false);
    }
}
