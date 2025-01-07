using UnityEngine;

public class PlayerController : PortalableObject
{
    //��ü�� ���� �� �ִ� ���̾�� ��������, �±׷� ����
    //��ü�� ��� ���� �� �ؾ���
    //��ü ���, ����
    public Texture2D crosshair;         //ũ�ν���� �̹��� 
    private float crosshairSize = 20f;  //ũ�ν���� ������

    private CameraMove cameraMove;      //ī�޶� �̵�

    private Collider heldCollider;      //���� ����ִ� ��ü�� �ݶ��̴�
    private Rigidbody heldrigidbody;    //���� ����ִ� ��ü�� ������ٵ�
    private GameObject heldObject;

    public Camera playerCamera;

    protected override void Awake()
    {
        // �θ� Ŭ������ Awake ���� ����
        base.Awake();
        // CameraMove ������Ʈ ��������
        cameraMove = GetComponent<CameraMove>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ���� ȭ�� �߾ӿ� ����
        Cursor.visible = false; // ���콺 Ŀ�� ����
    }

    private void Update()
    {
        
    }

    public void TryPickUp(GameObject obj)
    {
        RaycastHit hit;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            if (obj.tag == "Pick" && hit.collider != null && hit.rigidbody != null)
            {
                heldObject = obj;
                heldCollider.enabled = false;           //��ü �ε����� ����.
                heldrigidbody.isKinematic = false;      //��ü ȸ��X
            }
        }  
    }

    public override void Warp()
    {
        // �θ� Ŭ������ Warp ���� (��ġ, ȸ��, �ӵ� ��ȯ)
        base.Warp();
        // ī�޶� ȸ���� �ʱ�ȭ
        cameraMove.ResetTargetRotation();
    }
}