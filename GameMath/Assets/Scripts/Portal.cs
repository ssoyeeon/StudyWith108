
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    [field: SerializeField] public Portal OtherPortal { get; private set; }  // 연결된 다른 포털 참조 (Inspector에서 설정)   
    [field: SerializeField] public Color PortalColour { get; private set; } // 포털 색상   
    [SerializeField] private Renderer outlineRenderer;  // 포털 외곽선 렌더러       
    [SerializeField] private LayerMask placementMask;  // 포털 배치 가능한 레이어
    [SerializeField] private Transform testTransform;   // 포털 배치 테스트용 Transform    

    public bool IsPlaced { get; private set; } = false;   // 포털 설치 여부 
    private List<PortalableObject> portalObjects = new List<PortalableObject>();     // 포털 내부의 오브젝트 리스트 

    private Collider wallCollider;  // 포털이 설치된 벽 콜라이더   
    public Renderer Renderer { get; private set; }  // 컴포넌트 캐싱
    private new BoxCollider collider;

    void Awake()
    {
        // BoxCollider 컴포넌트 가져오기
        collider = GetComponent<BoxCollider>();

        // Renderer 컴포넌트 가져오기
        Renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // 포털 외곽선 색상 설정
        outlineRenderer.material.SetColor("_OutlineColour", PortalColour);

        // 초기에 포털 비활성화
        gameObject.SetActive(false);
    }

    void Update()
    {
        // 다른 포털이 설치된 경우에만 렌더러 활성화
        Renderer.enabled = OtherPortal.IsPlaced;

        // 포털 내부 오브젝트 체크
        for (int i = 0; i < portalObjects.Count; ++i)
        {
            Vector3 objPos = transform.InverseTransformPoint(portalObjects[i].transform.position);

            // 오브젝트가 포털을 통과했는지 확인
            if (objPos.z > 0.0f)
            {
                portalObjects[i].Warp();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // PortalableObject 컴포넌트 확인
        var obj = other.GetComponent<PortalableObject>();
        if (obj != null)
        {
            // 포털 오브젝트 리스트에 추가
            portalObjects.Add(obj);
            // 포털 진입 상태 설정
            obj.SetIsInPortal(this, OtherPortal, wallCollider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // PortalableObject 컴포넌트 확인
        var obj = other.GetComponent<PortalableObject>();

        // 리스트에 있는 객체라면 제거하고 포털 이탈 처리
        if (portalObjects.Contains(obj))
        {
            portalObjects.Remove(obj);
            obj.ExitPortal(wallCollider);
        }
    }
    private void FixOverhangs()
    {
        // 포털의 네 모서리 체크 위치
        var testPoints = new List<Vector3>
   {
       new Vector3(-1.1f,  0.0f, 0.1f),  // 좌측
       new Vector3( 1.1f,  0.0f, 0.1f),  // 우측
       new Vector3( 0.0f, -2.1f, 0.1f),  // 하단
       new Vector3( 0.0f,  2.1f, 0.1f)   // 상단
   };

        // 각 방향으로의 체크 방향
        var testDirs = new List<Vector3>
   {
        Vector3.right,    // 우
       -Vector3.right,    // 좌
        Vector3.up,       // 상
       -Vector3.up        // 하
   };

        // 각 모서리 위치에서 체크
        for (int i = 0; i < 4; ++i)
        {
            RaycastHit hit;
            // 월드 좌표로 변환
            Vector3 raycastPos = testTransform.TransformPoint(testPoints[i]);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            // 이미 표면과 겹치는지 체크
            if (Physics.CheckSphere(raycastPos, 0.05f, placementMask))
            {
                break;
            }
            // 레이캐스트로 가까운 표면 체크
            else if (Physics.Raycast(raycastPos, raycastDir, out hit, 2.1f, placementMask))
            {
                // 표면까지의 거리만큼 포털 위치 조정
                var offset = hit.point - raycastPos;
                testTransform.Translate(offset, Space.World);
            }
        }
    }
    private void FixIntersects()
    {
        // 체크할 방향들
        var testDirs = new List<Vector3>
   {
        Vector3.right,    // 우측
       -Vector3.right,    // 좌측
        Vector3.up,       // 상단
       -Vector3.up        // 하단
   };

        // 각 방향의 체크 거리
        var testDists = new List<float> { 1.1f, 1.1f, 2.1f, 2.1f };

        for (int i = 0; i < 4; ++i)
        {
            RaycastHit hit;
            // 포털 중심에서 약간 뒤에서 시작
            Vector3 raycastPos = testTransform.TransformPoint(0.0f, 0.0f, -0.1f);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            // 레이캐스트로 벽 체크
            if (Physics.Raycast(raycastPos, raycastDir, out hit, testDists[i], placementMask))
            {
                // 벽과의 거리만큼 반대 방향으로 이동
                var offset = (hit.point - raycastPos);
                var newOffset = -raycastDir * (testDists[i] - offset.magnitude);
                testTransform.Translate(newOffset, Space.World);
            }
        }
    }
    private bool CheckOverlap()
    {
        // 체크할 영역의 크기
        var checkExtents = new Vector3(0.9f, 1.9f, 0.05f);

        // 체크할 위치들
        var checkPositions = new Vector3[]
        {
       // 포털 중심
       testTransform.position + testTransform.TransformVector(new Vector3(0.0f, 0.0f, -0.1f)),

       // 포털의 네 모서리
       testTransform.position + testTransform.TransformVector(new Vector3(-1.0f, -2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(-1.0f, 2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(1.0f, -2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(1.0f, 2.0f, -0.1f)),

       // 깊이 방향 체크용 벡터
       testTransform.TransformVector(new Vector3(0.0f, 0.0f, 0.2f))
        };

        // 벽과의 교차 확인
        var intersections = Physics.OverlapBox(checkPositions[0], checkExtents, testTransform.rotation, placementMask);

        // 교차하는 콜라이더가 2개 이상이면 배치 불가
        if (intersections.Length > 1)
        {
            return false;
        }
        else if (intersections.Length == 1)
        {
            // 기존 포털 위치와의 교차는 허용
            if (intersections[0] != collider)
            {
                return false;
            }
        }

        // 모든 모서리가 표면에 걸쳐있는지 확인
        bool isOverlapping = true;
        for (int i = 1; i < checkPositions.Length - 1; ++i)
        {
            isOverlapping &= Physics.Linecast(checkPositions[i],
                checkPositions[i] + checkPositions[checkPositions.Length - 1], placementMask);
        }

        return isOverlapping;
    }
    public bool PlacePortal(Collider wallCollider, Vector3 pos, Quaternion rot)
    {
        testTransform.position = pos;
        testTransform.rotation = rot;
        testTransform.position -= testTransform.forward * 0.001f;

        FixOverhangs();
        FixIntersects();

        if (CheckOverlap())
        {
            this.wallCollider = wallCollider;
            transform.position = testTransform.position;
            transform.rotation = testTransform.rotation;

            gameObject.SetActive(true);
            IsPlaced = true;
            return true;
        }

        return false;
    }
    public void RemovePortal()
    {
        // 포털 비활성화
        gameObject.SetActive(false);
        // 설치 상태 false로 변경
        IsPlaced = false;
    }
}