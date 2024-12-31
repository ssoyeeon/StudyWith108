using UnityEngine;

[RequireComponent(typeof(MeshFilter))]       // 메시 필터 필수      //얘네가 꼭 이 스크립트에 있어야 한다!!
[RequireComponent(typeof(MeshRenderer))]     // 메시 렌더러 필수
[RequireComponent(typeof(Rigidbody))]        // 리지드바디 필수
[RequireComponent(typeof(Collider))]         // 콜라이더 필수
public class PortalableObject : MonoBehaviour
{
    private GameObject cloneObject;          // 포털에서 보이는 복제 오브젝트
    private int inPortalCount = 0;           // 현재 진입한 포털 수 카운트

    private Portal inPortal;                 // 진입한 포털
    private Portal outPortal;                // 목적지 포털
    private new Rigidbody rigidbody;        // 캐싱된 리지드바디
    protected new Collider collider;         // 캐싱된 콜라이더

    // 포털 통과시 180도 회전을 위한 상수
    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    protected virtual void Awake()     //이걸 나중에 다른 스크립트에서 상속을 받았을 때 오버라이드로 재선언 가능
    {
        // 복제 오브젝트 생성 및 설정
        cloneObject = new GameObject();
        cloneObject.SetActive(false);

        // 메시 필터와 렌더러 복사
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        // 컴포넌트 캐싱
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        // 포털이 없으면 업데이트 필요 없음
        if (inPortal == null || outPortal == null)
        {
            return;
        }

        // 복제 오브젝트가 활성화 상태이고 양쪽 포털이 설치된 경우
        if (cloneObject.activeSelf && inPortal.IsPlaced && outPortal.IsPlaced)
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            // 포털 기준으로 복제 오브젝트 위치 계산
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            // 포털 기준으로 복제 오브젝트 회전 계산
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;
        }
        else
        {
            // 포털이 없거나 비활성화 상태면 복제 오브젝트를 멀리 이동
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        // 진입한 포털 정보 설정
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        // 포털이 설치된 벽과의 충돌 무시
        Physics.IgnoreCollision(collider, wallCollider);

        // 복제 오브젝트 비활성화
        cloneObject.SetActive(false);

        // 포털 진입 카운트 증가
        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        // 벽과의 충돌 다시 활성화
        Physics.IgnoreCollision(collider, wallCollider, false);

        // 포털 진입 카운트 감소
        --inPortalCount;

        // 모든 포털에서 나왔으면 복제 오브젝트 비활성화
        if (inPortalCount == 0)
        {
            cloneObject.SetActive(false);
        }
    }

    public virtual void Warp()
    {
        var inTransform = inPortal.transform;
        var outTransform = outPortal.transform;

        // 오브젝트 위치 변환
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
        relativePos = halfTurn * relativePos;
        transform.position = outTransform.TransformPoint(relativePos);

        // 오브젝트 회전 변환
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = halfTurn * relativeRot;
        transform.rotation = outTransform.rotation * relativeRot;

        // 리지드바디 속도 변환
        Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);
        relativeVel = halfTurn * relativeVel;
        rigidbody.velocity = outTransform.TransformDirection(relativeVel);

        // 입구/출구 포털 참조 교체
        var tmp = inPortal;
        inPortal = outPortal;
        outPortal = tmp;
    }
}