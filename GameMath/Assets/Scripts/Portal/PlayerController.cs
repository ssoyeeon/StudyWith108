using UnityEngine;

public class PlayerController : PortalableObject
{
    //물체를 잡을 수 있는 레이어는 하지말고, 태그로 변경
    //물체를 잡고 놓을 때 해야해
    //물체 잡기, 놓기
    public Texture2D crosshair;         //크로스헤어 이미지 
    private float crosshairSize = 20f;  //크로스헤어 사이즈

    private CameraMove cameraMove;      //카메라 이동

    private Collider heldCollider;      //현재 들고있는 물체의 콜라이더
    private Rigidbody heldrigidbody;    //현재 들고있는 물체의 리지드바디
    private GameObject heldObject;

    public Camera playerCamera;

    protected override void Awake()
    {
        // 부모 클래스의 Awake 실행 재사용
        base.Awake();
        // CameraMove 컴포넌트 가져오기
        cameraMove = GetComponent<CameraMove>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 화면 중앙에 고정
        Cursor.visible = false; // 마우스 커서 숨김
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
                heldCollider.enabled = false;           //물체 부딪히지 않음.
                heldrigidbody.isKinematic = false;      //물체 회전X
            }
        }  
    }

    public override void Warp()
    {
        // 부모 클래스의 Warp 실행 (위치, 회전, 속도 변환)
        base.Warp();
        // 카메라 회전값 초기화
        cameraMove.ResetTargetRotation();
    }
}