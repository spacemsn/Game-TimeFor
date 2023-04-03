using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoCache
{
    [Header("���������� ������")]
    [SerializeField] private Animator animator;
    [SerializeField] public Camera camera;
    [SerializeField] private Rigidbody rb;

    [Header("CharacterStatus")]
    public Vector3 position;

    [Header("CharacterPrefab")]
    public GameObject _characterPrefab; 

    [Header("CharacterLevel")]
    public int levelId;

    [Header("CharacterIndicators")]
    public CharacterIndicators _indicators;
    public int health;
    public float stamina;

    [Header("PlayerMove")]
    public CharacterMove move;

    [Header("CharacterAbilities")]
    public CharacterAbilities characterAbilities;

    [Header("�������������� ���������")]
    [SerializeField] Vector3 movement;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float runSpeed = 3.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float debuff = 0.15f;
    [SerializeField] private float minStaminaToRun = 25f;
    [SerializeField] public float smoothTime;
    [SerializeField] private float smoothVelocity;
    [HideInInspector] public bool charMenegment = true;
    [SerializeField] private bool isGrounded = true;

    private void Start()
    {
        #region Components

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        #endregion

        #region GameObject

        _characterPrefab = this.gameObject;

        #endregion

        #region CharacterLevel

        levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        position = this.transform.position;

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        #endregion

        #region CharacterAbilities

        characterAbilities = this.GetComponent<CharacterAbilities>();

        #endregion

        #region PlayerGtp

        move = this.GetComponent<CharacterMove>();

        #endregion
    }

    private void UpdateStatus()
    {
        #region CharacterLevel

        levelId = SceneManager.GetActiveScene().buildIndex;

        #endregion

        #region CharacterStatus

        position = this.transform.position;

        #endregion

        #region CharacterIndicators

        _indicators = this.GetComponent<CharacterIndicators>();

        #endregion

        #region PlayerGtp

        move = this.GetComponent<CharacterMove>();

        #endregion
    }

    private void Update()
    {
        Status();
    }

    private void FixedUpdate()
    {
        Movement();
        UpdateStatus();
    }

    private void Movement() // for rigidbody
    {
        if (charMenegment)
        {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (movement.magnitude > Mathf.Abs(0.05f))
            {
                float rotationAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                movement = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            }

            // ������
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jump", true);
                isGrounded = false;
            }

            move.Move(movement, stamina, runSpeed, moveSpeed, debuff, maxStamina);
        }
        else
        {
            //Stop Moving/Translating
            rb.velocity = Vector3.zero;

            //Stop rotating
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void LookAt(GameObject currentEnemy)
    {
        if (charMenegment)
        {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (movement.magnitude > Mathf.Abs(0.05f))
            {
                float rotationAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + currentEnemy.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                movement = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jump", true);
                isGrounded = false;
            }

            //transform.LookAt(currentEnemy.transform.position, Vector3.up);
            move.Move(movement, stamina, runSpeed, moveSpeed, debuff, maxStamina);
        }
        else
        {
            //Stop Moving/Translating
            rb.velocity = Vector3.zero;

            //Stop rotating
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
    }

    private void Status()
    {
        _indicators.Indicators(health, stamina);
    }

    #region Save and Load Json
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();

        levelId = data.level;
        health = data.health;
        stamina = data.stamina;
        move.TransportPlayer(data.position);
    }
    #endregion
}
