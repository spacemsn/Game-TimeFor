using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class indicatorCharacter : MonoCache, IElementBehavior, IDamageBehavior
{
    public static Action onLevelUp;

    [Header("EntryPoint")]
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("�������������� ������")]
    [Header("�����")]
    public string playerName;
    [Header("������� �����")]
    public int LevelScene;
    [Header("������� ������")]
    public int levelPlayer;
    [Header("������� ����")]
    public int experience;
    [Header("��������� ����")]
    public int experienceRequired;
    [Header("��������� ����")]
    public int elementalPoints;
    [Header("��������")]
    public float health;
    [Header("������������ ��������")]
    public float healthMax;
    [Header("�������������")]
    public float stamina;
    [Header("������������ ������������")]
    public float staminaMax;
    [Header("������� ���� ����")]
    public float damageBaseFire;
    [Header("������� ���� ����")]
    public float damageBaseWater;
    [Header("������� ���� �������")]
    public float damageBaseAir;
    [Header("������� ���� �����")]
    public float damageBaseTerra;
    [Header("������� �� ����� ����")]
    public float damagePercentFire;
    [Header("������� �� ����� ����")]
    public float damagePercentWater;
    [Header("������� �� ����� �������")]
    public float damagePercentAir;
    [Header("������� �� ����� �����")]
    public float damagePercentTerra;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Image reactionImage;
    [SerializeField] TMP_Text damageText;
    [SerializeField] DeathScript dealthCharacter;
    [SerializeField] mainCharacter status;

    [Header("�������� ������")]
    public Sprite WaterSprite;
    public Sprite FireSprite;
    public Sprite AirSprite;
    public Sprite TerraSprite;

    [Header("�������� ������� ������")]
    public Sprite damageUpSprite;
    public Sprite VisionDownSprite;
    public Sprite MovementDownSprite;

    [Header("������ ������")]
    public Elements currentStatus;

    [Header("������� ������")]
    public Reactions reaction;

    [Header("����� ��������� �������")]
    public float timeStatus;
    public bool runStatusCorouutine;

    [Header("����� ��������� �������")]
    public float timeReaction;
    public bool runReactionCorouutine;


    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (value > healthMax)
            {
                value = healthMax;
            }
            if (value <= 0)
            {
                dealthCharacter.OpenMenu();
            }
        }
    }

    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = value;

            if (value > staminaMax)
            {
                value = staminaMax;
            }
            else if (value < 0)
            {
                value = 0;
            }
        }
    }

    private void Start()
    {
        status = GetComponent<mainCharacter>();

        #region Resources 

        WaterSprite = Resources.Load<Sprite>("UI/Sprites/Elements/waterIcon");
        FireSprite = Resources.Load<Sprite>("UI/Sprites/Elements/fireIcon");
        AirSprite = Resources.Load<Sprite>("UI/Sprites/Elements/airIcon");
        TerraSprite = Resources.Load<Sprite>("UI/Sprites/Elements/terraIcon");

        damageUpSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/damageUpImage");
        VisionDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/visionDebuffImage");
        MovementDownSprite = Resources.Load<Sprite>("UI/Sprites/Reactions/movementDebuffImage");

        #endregion

        EnemyBehavior.onDeadEnemy += GainExperience;
        Chest.onOpenChest += GainExperience;
        QuestReward.onQuestReward += GainExperience;

        SpawnContoller.onPlayerSceneLoaded += LoadStatus;
    }

    private void OnDisable()
    {
        EnemyBehavior.onDeadEnemy -= GainExperience;
        Chest.onOpenChest -= GainExperience;
        QuestReward.onQuestReward -= GainExperience;

        SpawnContoller.onPlayerSceneLoaded -= LoadStatus;
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        healthBar = uI.healthBar;
        staminaBar = uI.staminaBar;
        reactionImage = uI.reactionImage;
        damageText = uI.damageText;

        reactionImage.enabled = false;
        damageText.enabled = false;
    }

    private void Update()
    {
        SetIcon();
    }

    public void GainExperience(int amount) // ����� ��������� ������ 
    {
        experience += amount;
        if (experience >= experienceRequired)
        {
            LevelUp();
        }
    }

    private void LevelUp() // ����� �������� ������ 
    {
        levelPlayer++;
        experience -= experienceRequired;
        experienceRequired = Mathf.RoundToInt(experienceRequired * 1.5f);
        healthMax += 50;
        elementalPoints++;

        UpdateStats();

        Debug.Log("Level Up! Current Level: " + levelPlayer);

        onLevelUp.Invoke();
    }

    public void UpdateStats()
    {
        // �������� ������������ ���������� � UI 
        healthBar.maxValue = healthMax;
        staminaBar.maxValue = staminaMax;

        health = healthMax; stamina = staminaMax;
        healthBar.value = health; staminaBar.value = stamina;
    }

    public void LoadStatus()
    {
        // �������� ������������ ���������� � UI 
        healthBar.maxValue = healthMax;
        staminaBar.maxValue = staminaMax;

        health = healthMax; stamina = staminaMax;
    }

    private void SetIcon()
    {
        if (reaction == Reactions.Null)
        {
            switch (currentStatus)
            {
                case Elements.Water:
                    reactionImage.sprite = WaterSprite;
                    reactionImage.enabled = true;
                    break;

                case Elements.Fire:
                    reactionImage.sprite = FireSprite;
                    reactionImage.enabled = true;
                    break;

                case Elements.Air:
                    reactionImage.sprite = AirSprite;
                    reactionImage.enabled = true;
                    break;

                case Elements.Terra:
                    reactionImage.sprite = TerraSprite;
                    reactionImage.enabled = true;
                    break;

                case Elements.Null:
                    reactionImage.enabled = false;
                    break;
            }
        }
        if (currentStatus == Elements.Null)
        {
            switch (reaction)
            {
                case Reactions.DamageUp:
                    {
                        reactionImage.sprite = damageUpSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case Reactions.MovementDown:
                    {
                        reactionImage.sprite = MovementDownSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case Reactions.VisionDown:
                    {
                        reactionImage.sprite = VisionDownSprite;
                        reactionImage.enabled = true;
                        break;
                    }

                case Reactions.Null:
                    {
                        reactionImage.enabled = false;
                        break;
                    }
            }
        }
    }

    public void Reaction(Elements secondary, float buff, float damage)
    {
        if ((currentStatus == Elements.Water && secondary == Elements.Fire) || (currentStatus == Elements.Fire && secondary == Elements.Water))
        {
            Debug.Log("������ ���������� �����!");
            reaction = Reactions.DamageUp;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            damage *= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == Elements.Terra && secondary == Elements.Fire) || (currentStatus == Elements.Water && secondary == Elements.Terra))
        {
            Debug.Log("������ ��������� ��������");
            reaction = Reactions.MovementDown;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            //navAgent.speed -= buff;
            TakeDamage(damage);
        }
        else if ((currentStatus == Elements.Fire && secondary == Elements.Air) || (currentStatus == Elements.Water && secondary == Elements.Air) || (currentStatus == Elements.Terra && secondary == Elements.Air))
        {
            Debug.Log("������ ��������� ������");
            reaction = Reactions.VisionDown;
            SetDefauntStatus();
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitReaction(timeReaction));
            }
            else
            {
                StopCoroutine(WaitReaction(timeReaction));
                StartCoroutine(WaitReaction(timeReaction));
            }
            //viewAngle -= buff;
            TakeDamage(damage);
        }
        else
        {
            currentStatus = secondary;
            TakeDamage(damage);
            if (runStatusCorouutine)
            {
                StartCoroutine(WaitStatus(timeStatus));
            }
            else
            {
                StopCoroutine(WaitStatus(timeStatus));
                StartCoroutine(WaitStatus(timeStatus));
            }
        }
    }

    IEnumerator WaitStatus(float timeStatus)
    {
        runStatusCorouutine = true;

        yield return new WaitForSeconds(timeStatus);
        currentStatus = Elements.Null;
        runStatusCorouutine = false;
    }

    IEnumerator WaitReaction(float timeStatus)
    {
        runReactionCorouutine = true;

        yield return new WaitForSeconds(timeStatus);
        reaction = Reactions.Null;
        runReactionCorouutine = false;
    }

    void SetDefauntStatus()
    {
        currentStatus = Elements.Null;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage; healthBar.value = Health;
    }

    public void SetHealth(float bonushealth)
    {
        Health += bonushealth; healthBar.value = Health;
    }

    public void SetMaxHealth(float bonushealth)
    {
        healthMax += bonushealth; healthBar.value = Health;
    }

    public void TakeStamina(float amount)
    {
        Stamina -= amount; staminaBar.value = Stamina;
    }

    public void SetStamina(float bonusstamina)
    {
        Stamina += bonusstamina; staminaBar.value = Stamina;
    }

    public void SetMaxStamina(float bonusstamina)
    {
        staminaMax += bonusstamina; staminaBar.value = Stamina;
    }

    public int GetSceneIndex()
    {
        LevelScene = SceneManager.GetActiveScene().buildIndex;
        return LevelScene;
    }
}
