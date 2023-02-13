using UnityEngine;

public class CharacterMove : MonoCache
{
    [Header("����������")]
    [SerializeField] CharacterStatus status;
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] Transform _camera;
    [SerializeField] CharacterIndicators indicators;
    [SerializeField] CharacterAbilities abilities;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Vector3 playerVelocity;

    private void Start()
    {
        indicators = GetComponent<CharacterIndicators>();
        abilities = GetComponent<CharacterAbilities>();    
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraManager = GetComponent<CameraManager>();
        _camera = cameraManager._camera.transform;
    }

    public void MovePC(Vector3 moveDirection, float stamina, float jumpValue, float gravity, float smoothTime, float smoothVelocity, float walkingSpeed, float runningSpeed, float normallSpeed, float debuff, bool charMenegment)
    {
        if (charMenegment == true)
        {
            animator.SetFloat("StandartMotion", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

            if (abilities.aimMode == false)
            {
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }
            else if (abilities.aimMode == true)
            {
                float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) + _camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                if (moveDirection.magnitude > Mathf.Abs(0.05f))
                {
                    moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > Mathf.Abs(0.05f)) // ���
            {
                indicators.TakeStamina(debuff * 2);
                if (stamina > 0)
                {
                    controller.Move(moveDirection.normalized * runningSpeed * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                }
            }
            else // ������� ���������
            {
                controller.Move(moveDirection.normalized * walkingSpeed * Time.deltaTime);
                indicators.SetStamina(debuff);
            }

            if (Input.GetButton("Jump") && controller.isGrounded) // ������
            {
                playerVelocity.y = Mathf.Sqrt(jumpValue * -2.0f * gravity);
                controller.Move(playerVelocity * Time.deltaTime);
            }
            else
            {
                playerVelocity.y += gravity * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);
            }
        }
        else if (charMenegment == false)
        {
            
        }

    } // �������� ��������� �� ������

    //private void SimpleMove() // ���������� ���������� ��� ������� � ��
    //{

    //    deltaH = Input.GetAxisRaw("Horizontal");
    //    deltaV = Input.GetAxisRaw("Vertical");


    //    Vector3 moveDirection = new Vector3(deltaH, 0, deltaV);
    //    animator.SetFloat("isRun", Vector3.ClampMagnitude(moveDirection, 1).magnitude);

    //    if (moveDirection.magnitude > Mathf.Abs(0.05f))
    //    {
    //        float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref smoothVelocity, smoothTime);
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //        Vector3 moveCharacter = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

    //        controller.SimpleMove(Vector3.ClampMagnitude(moveCharacter, 1) * walkingSpeed);
    //    }
    //}
}
