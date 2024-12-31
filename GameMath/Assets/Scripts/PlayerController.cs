using UnityEngine;

public class PlayerController : PortalableObject
{
    private CameraMove cameraMove;

    protected override void Awake()
    {
        // �θ� Ŭ������ Awake ���� ����
        base.Awake();
        // CameraMove ������Ʈ ��������
        cameraMove = GetComponent<CameraMove>();
    }

    public override void Warp()
    {
        // �θ� Ŭ������ Warp ���� (��ġ, ȸ��, �ӵ� ��ȯ)
        base.Warp();
        // ī�޶� ȸ���� �ʱ�ȭ
        cameraMove.ResetTargetRotation();
    }
}