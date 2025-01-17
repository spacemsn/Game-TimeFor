using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class EnemyBehavior : MonoBehaviour, IMoveBehavior
{
    public static Action<int> onDeadEnemy;

    [Header("�������������� �����")]
    [SerializeField] EnemyObject enemyParam;
    [HideInInspector] public float viewAngle;
    [HideInInspector] public float viewDistance;
    [SerializeField] private float chaseTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float distanceMax;
    [SerializeField] private float originPos;
    [SerializeField] private float playerPos;
    [SerializeField] private bool isTrigger;

    [Header("����� �����")]
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private int currentPoint = 0;

    [Header("���������� �����")]
    private Animator animator;
    private NavMeshAgent navAgent;
    public Transform player;
    private Vector3 originalPosition;
    [HideInInspector] public Transform centerOfEnemy;

    // ��������� �����
    public enum EnemyStage 
    { 
        Wait, 
        Patrolling, 
        Trigger, 
        Chase, 
        Attack, 
        Death, 
    }
    [Header("������ ��������� �����")]
    [SerializeField] public EnemyStage currentState;

    [Header("������� �����")]
    public IMoveBehavior.EnemyLevel enemyLevel;

    [Header("������� �����")]
    public int experience;

    void Start()
    {
        //������� ��������� NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        navAgent.isStopped = false;
        currentState = EnemyStage.Wait;
        enemyParam.SetBehavior(this);

        centerOfEnemy = gameObject.transform.Find("CenterOfEnemy");

        //���������� ��������� �������
        originalPosition = transform.position;

    }

    void Update()
    {
        originPos = Vector3.Distance(transform.position, originalPosition);
        if (player != null) { playerPos = Vector3.Distance(transform.position, player.position); }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        switch (currentState)
        {
            case EnemyStage.Wait:
                {
                    navAgent.isStopped = true;
                    animator.SetTrigger("Wait");
                    if (CanSeePlayer() == EnemyStage.Patrolling)
                    {
                        ChangeState(EnemyStage.Patrolling);
                    }
                    else if (CanSeePlayer() == EnemyStage.Chase)
                    {
                        ChangeState(EnemyStage.Chase);
                    }
                    break;
                }

            case EnemyStage.Patrolling:
                {
                    navAgent.isStopped = false;
                    animator.SetTrigger("Move");
                    navAgent.SetDestination(movePoints[currentPoint].position);
                    if (Vector3.Distance(transform.position, movePoints[currentPoint].position) < 1.5f)
                    {
                        currentPoint++;
                        if (currentPoint >= movePoints.Length)
                        {
                            currentPoint = 0;
                        }
                    }
                    else if (CanSeePlayer() == EnemyStage.Chase)
                    {
                        ChangeState(EnemyStage.Chase);
                    }
                    break;
                }

            case EnemyStage.Trigger:
                {

                    break;
                }

            case EnemyStage.Chase:
                {
                    navAgent.isStopped = false;
                    if (playerPos <= 2f)
                    {
                        ChangeState(EnemyStage.Attack);
                    }
                    //else if (originPos >= distanceMax)
                    //{
                    //    chaseTime -= Time.deltaTime;
                    //    navAgent.SetDestination(player.position);
                    //    animator.SetTrigger("Chase");

                    //    if (chaseTime <= 0)
                    //    {
                    //        enemyStage = EnemyStage.Patrolling;
                    //    }
                    //}
                    else
                    {
                        chaseTime = 5f;
                        navAgent.SetDestination(player.position);
                        animator.SetTrigger("Chase");
                    }
                    break;
                }

            case EnemyStage.Attack:
                {
                    navAgent.isStopped = true;
                    animator.SetTrigger("Attack");
                    gameObject.transform.LookAt(player.transform);

                    if (playerPos >= 3f)
                    {
                        ChangeState(EnemyStage.Chase);
                    }
                    break;
                }

            case EnemyStage.Death:
                {
                    navAgent.isStopped = true;
                    animator.SetTrigger("Death");
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    Destroy(this.gameObject, 5f);
                    break;
                }
        }
    }

    public void ChangeState(EnemyStage newState)
    {
        currentState = newState;
    }

    private EnemyStage CanSeePlayer()
    {
        if (player != null)
        {
            //������� ����������� �� ������
            Vector3 direction = player.position - transform.position;

            //������� ���� ����� ������������ �� ������ � ������������ ������� �����
            float angle = Vector3.Angle(direction, transform.forward);

            //���� ���� ������ ��� ����� ���� ������ � ���������� �� ������ ������ ��� ����� ���������� �����������, �� ����� ���������
            if (angle <= viewAngle && Vector3.Distance(transform.position, player.position) <= viewDistance || Vector3.Distance(transform.position, player.position) <= viewDistance)
            {
                return EnemyStage.Chase;
            }

            return EnemyStage.Patrolling;
        }
        else { return EnemyStage.Patrolling; }
    }

    public void DeadEnemy()
    {
        onDeadEnemy.Invoke(experience);
    }

    private void OnDestroy()
    {
        if(player != null)
        {
            player.GetComponent<QuestManager>().KillEnemy(enemyParam.objectPrefab);
        }
    }
}