using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{
    private const float moveSpeed = 7.5f;    // �̵� �ӵ�
    private const float cameraSpeed = 3.0f;  // ȸ�� �ӵ�

    // ��ǥ ȸ���� (�ܺο��� �б⸸ ����)
    public Quaternion TargetRotation { private set; get; }

    private Vector3 moveVector = Vector3.zero;
    private float moveY = 0.0f;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ���
        TargetRotation = transform.rotation;
    }

    private void Update()
    {
        // ī�޶� ȸ��
        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;

        // ���� ȸ�� ���� (-75�� ~ 75��)
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);

        TargetRotation = Quaternion.Euler(targetEuler);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation,
            Time.deltaTime * 15.0f);

        // �̵� �Է�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVector = new Vector3(x, 0.0f, z) * moveSpeed;

    }

    private void FixedUpdate()
    {
        // �̵� ����
        Vector3 newVelocity = transform.TransformDirection(moveVector);
        rigidbody.velocity = newVelocity;
    }

    public void ResetTargetRotation()
    {
        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }
}