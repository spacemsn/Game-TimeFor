using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGTP : MonoBehaviour
{
    [Header("�������������� �����")]
    [SerializeField] EnemyObject enemyParam;
    private float hp = 100;
    private int enemyDamage;
    private float viewAngle = 90f;
    private float viewDistance = 50f;

    [Header("���������� �����")] //���������� ��� ���������� MavMeshAgent
    private Vector3 originalPosition;
    private NavMeshAgent navAgent;
    public CapsuleCollider rightHand;
    public CapsuleCollider leftHand;
    public Animator animator;
    public Slider healthBar;
    private Transform player;
    public Transform centerOfEnemy;

    //���������� ��� �����, �� ������� ����� ��������� ����
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private float distanceMax = 30f;

    //���������� ��� ���������� ������ � ������� �������������
    [SerializeField] private float chaseTime = 5f; // ����� ������������� ������
    [SerializeField] private float originPos; // ���������� ����� ������ � ��� �������������� ������
    [SerializeField] private float playerPos; // ���������� ����� ������ � �������

    private enum EnemyBehavior { Wait, Patrolling, Chase, Attack, Death, }
    [SerializeField] private EnemyBehavior enemyBehavior;

    void Start()
    {
        SetParam();

        enemyBehavior = EnemyBehavior.Wait;

        rightHand.enabled = false;
        leftHand.enabled = false;

        //������� ��������� NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        //������� ������
        player = GameObject.FindGameObjectWithTag("Player").transform;
        centerOfEnemy = gameObject.transform.Find("CenterOfEnemy");

        //���������� ��������� �������
        originalPosition = transform.position;
    }

    private void SetParam()
    {
        hp = enemyParam.hp;
        enemyDamage = enemyParam.enemyDamage;
        viewAngle = enemyParam.viewAngle;
        viewDistance = enemyParam.viewDistance;
    }

    void Update()
    {
        healthBar.value = hp;
        originPos = Vector3.Distance(transform.position, originalPosition);
        playerPos = Vector3.Distance(transform.position, player.position);

        switch (enemyBehavior)
        {
            case EnemyBehavior.Wait:
                {
                    if (CanSeePlayer() == EnemyBehavior.Patrolling)
                    {
                        enemyBehavior = EnemyBehavior.Patrolling;
                        animator.SetTrigger("Move");
                    }
                    else if (CanSeePlayer() == EnemyBehavior.Chase)
                    {
                        enemyBehavior = EnemyBehavior.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyBehavior.Patrolling:
                {
                    navAgent.destination = movePoints[currentPoint].position;

                    //���� �������� �����, �� ��������� � ���������
                    if (Vector3.Distance(transform.position, movePoints[currentPoint].position) < 1.5f)
                    {
                        currentPoint++;
                        if (currentPoint >= movePoints.Length)
                        {
                            currentPoint = 0;
                        }
                    }
                    if (CanSeePlayer() == EnemyBehavior.Chase)
                    {
                        enemyBehavior = EnemyBehavior.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyBehavior.Chase:
                {
                    if (playerPos <= navAgent.stoppingDistance)
                    {
                        enemyBehavior = EnemyBehavior.Attack;
                        animator.SetTrigger("Attack");
                    }
                    else if (originPos >= distanceMax)
                    { 
                        chaseTime -= Time.deltaTime;
                        navAgent.destination = player.position;

                        if (chaseTime <= 0)
                        { 
                            enemyBehavior = EnemyBehavior.Patrolling;
                            animator.SetTrigger("Move");
                        }
                    }
                    else
                    {
                        chaseTime = 5f;
                        navAgent.destination = player.position;
                    }
                }
                break;

            case EnemyBehavior.Attack:
                {
                    if (playerPos <= navAgent.stoppingDistance)
                    {
                        gameObject.transform.LookAt(player.transform);
                    }
                    else 
                    { 
                        enemyBehavior = EnemyBehavior.Chase;
                        animator.SetTrigger("Chase");
                    }
                }
                break;

            case EnemyBehavior.Death:
                {
                    navAgent.Stop();
                    animator.SetTrigger("Death");
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    healthBar.gameObject.SetActive(false);
                    Destroy(this.gameObject, 10f);
                }
                break;

            default:
                { break; }
        }
    }

    private EnemyBehavior CanSeePlayer()
    {
        //������� ����������� �� ������
        Vector3 direction = player.position - transform.position;

        //������� ���� ����� ������������ �� ������ � ������������ ������� �����
        float angle = Vector3.Angle(direction, transform.forward);

        //���� ���� ������ ��� ����� ���� ������ � ���������� �� ������ ������ ��� ����� ���������� �����������, �� ����� ���������
        if (angle <= viewAngle && Vector3.Distance(transform.position, player.position) <= viewDistance || Vector3.Distance(transform.position, player.position) <= viewDistance)
        {
            return EnemyBehavior.Chase;
        }

        return EnemyBehavior.Patrolling;
    }

    public void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            enemyBehavior = EnemyBehavior.Death;
        }
        else
        {
            //animator.SetTrigger("damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterIndicators indicators = other.gameObject.GetComponent<CharacterIndicators>();
        if (indicators)
        {
            indicators.TakeHit(enemyDamage);
        }
    }

    public void CollidersTrue()
    {
        //rightHand.enabled = true;
        leftHand.enabled = true;
    }

    public void CollidersFalse()
    {
        //rightHand.enabled = false;
        leftHand.enabled = false;
    }
}
