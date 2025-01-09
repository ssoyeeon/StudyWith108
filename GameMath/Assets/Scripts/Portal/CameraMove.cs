using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{
    private const float moveSpeed = 7.5f;    // 이동 속도
    private const float cameraSpeed = 3.0f;  // 회전 속도

    // 목표 회전값 (외부에서 읽기만 가능)
    public Quaternion TargetRotation { private set; get; }

    private Vector3 moveVector = Vector3.zero;
    private float moveY = 0.0f;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 잠금
        TargetRotation = transform.rotation;
    }

    private void Update()
    {
        // 카메라 회전
        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;

        // 수직 회전 제한 (-75도 ~ 75도)
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);

        TargetRotation = Quaternion.Euler(targetEuler);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation,
            Time.deltaTime * 15.0f);

        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVector = new Vector3(x, 0.0f, z) * moveSpeed;

    }

    private void FixedUpdate()
    {
        // 이동 적용
        Vector3 newVelocity = transform.TransformDirection(moveVector);
        rigidbody.velocity = newVelocity;
    }

    public void ResetTargetRotation()
    {
        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }
}