using UnityEngine;

public class PlayerController : PortalableObject
{
    private CameraMove cameraMove;

    protected override void Awake()
    {
        // 부모 클래스의 Awake 실행 재사용
        base.Awake();
        // CameraMove 컴포넌트 가져오기
        cameraMove = GetComponent<CameraMove>();
    }

    public override void Warp()
    {
        // 부모 클래스의 Warp 실행 (위치, 회전, 속도 변환)
        base.Warp();
        // 카메라 회전값 초기화
        cameraMove.ResetTargetRotation();
    }
}