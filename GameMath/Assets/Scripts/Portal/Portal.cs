
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class Portal : MonoBehaviour
{
    [field: SerializeField] public Portal OtherPortal { get; private set; }  // ����� �ٸ� ���� ���� (Inspector���� ����)   
    [field: SerializeField] public Color PortalColour { get; private set; } // ���� ����   
    [SerializeField] private Renderer outlineRenderer;  // ���� �ܰ��� ������       
    [SerializeField] private LayerMask placementMask;  // ���� ��ġ ������ ���̾�
    [SerializeField] private Transform testTransform;   // ���� ��ġ �׽�Ʈ�� Transform    

    public bool IsPlaced { get; private set; } = false;   // ���� ��ġ ���� 
    private List<PortalableObject> portalObjects = new List<PortalableObject>();     // ���� ������ ������Ʈ ����Ʈ 

    private Collider wallCollider;  // ������ ��ġ�� �� �ݶ��̴�   
    public Renderer Renderer { get; private set; }  // ������Ʈ ĳ��
    private new BoxCollider collider;

    void Awake()
    {
        // BoxCollider ������Ʈ ��������
        collider = GetComponent<BoxCollider>();

        // Renderer ������Ʈ ��������
        Renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // ���� �ܰ��� ���� ����
        outlineRenderer.material.SetColor("_OutlineColour", PortalColour);

        // �ʱ⿡ ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    void Update()
    {
        // �ٸ� ������ ��ġ�� ��쿡�� ������ Ȱ��ȭ
        Renderer.enabled = OtherPortal.IsPlaced;

        // ���� ���� ������Ʈ üũ
        for (int i = 0; i < portalObjects.Count; ++i)
        {
            Vector3 objPos = transform.InverseTransformPoint(portalObjects[i].transform.position);

            // ������Ʈ�� ������ ����ߴ��� Ȯ��
            if (objPos.z > 0.0f)
            {
                portalObjects[i].Warp();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // PortalableObject ������Ʈ Ȯ��
        var obj = other.GetComponent<PortalableObject>();
        if (obj != null)
        {
            // ���� ������Ʈ ����Ʈ�� �߰�
            portalObjects.Add(obj);
            // ���� ���� ���� ����
            obj.SetIsInPortal(this, OtherPortal, wallCollider);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // PortalableObject ������Ʈ Ȯ��
        var obj = other.GetComponent<PortalableObject>();

        // ����Ʈ�� �ִ� ��ü��� �����ϰ� ���� ��Ż ó��
        if (portalObjects.Contains(obj))
        {
            portalObjects.Remove(obj);
            obj.ExitPortal(wallCollider);
        }
    }
    private void FixOverhangs()
    {
        // ������ �� �𼭸� üũ ��ġ
        var testPoints = new List<Vector3>
   {
       new Vector3(-1.1f,  0.0f, 0.1f),  // ����
       new Vector3( 1.1f,  0.0f, 0.1f),  // ����
       new Vector3( 0.0f, -2.1f, 0.1f),  // �ϴ�
       new Vector3( 0.0f,  2.1f, 0.1f)   // ���
   };

        // �� ���������� üũ ����
        var testDirs = new List<Vector3>
   {
        Vector3.right,    // ��
       -Vector3.right,    // ��
        Vector3.up,       // ��
       -Vector3.up        // ��
   };

        // �� �𼭸� ��ġ���� üũ
        for (int i = 0; i < 4; ++i)
        {
            RaycastHit hit;
            // ���� ��ǥ�� ��ȯ
            Vector3 raycastPos = testTransform.TransformPoint(testPoints[i]);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            // �̹� ǥ��� ��ġ���� üũ
            if (Physics.CheckSphere(raycastPos, 0.05f, placementMask))
            {
                break;
            }
            // ����ĳ��Ʈ�� ����� ǥ�� üũ
            else if (Physics.Raycast(raycastPos, raycastDir, out hit, 2.1f, placementMask))
            {
                // ǥ������� �Ÿ���ŭ ���� ��ġ ����
                var offset = hit.point - raycastPos;
                testTransform.Translate(offset, Space.World);
            }
        }
    }
    private void FixIntersects()
    {
        // üũ�� �����
        var testDirs = new List<Vector3>
   {
        Vector3.right,    // ����
       -Vector3.right,    // ����
        Vector3.up,       // ���
       -Vector3.up        // �ϴ�
   };

        // �� ������ üũ �Ÿ�
        var testDists = new List<float> { 1.1f, 1.1f, 2.1f, 2.1f };

        for (int i = 0; i < 4; ++i)
        {
            RaycastHit hit;
            // ���� �߽ɿ��� �ణ �ڿ��� ����
            Vector3 raycastPos = testTransform.TransformPoint(0.0f, 0.0f, -0.1f);
            Vector3 raycastDir = testTransform.TransformDirection(testDirs[i]);

            // ����ĳ��Ʈ�� �� üũ
            if (Physics.Raycast(raycastPos, raycastDir, out hit, testDists[i], placementMask))
            {
                // ������ �Ÿ���ŭ �ݴ� �������� �̵�
                var offset = (hit.point - raycastPos);
                var newOffset = -raycastDir * (testDists[i] - offset.magnitude);
                testTransform.Translate(newOffset, Space.World);
            }
        }
    }
    private bool CheckOverlap()
    {
        // üũ�� ������ ũ��
        var checkExtents = new Vector3(0.9f, 1.9f, 0.05f);

        // üũ�� ��ġ��
        var checkPositions = new Vector3[]
        {
       // ���� �߽�
       testTransform.position + testTransform.TransformVector(new Vector3(0.0f, 0.0f, -0.1f)),

       // ������ �� �𼭸�
       testTransform.position + testTransform.TransformVector(new Vector3(-1.0f, -2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(-1.0f, 2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(1.0f, -2.0f, -0.1f)),
       testTransform.position + testTransform.TransformVector(new Vector3(1.0f, 2.0f, -0.1f)),

       // ���� ���� üũ�� ����
       testTransform.TransformVector(new Vector3(0.0f, 0.0f, 0.2f))
        };

        // ������ ���� Ȯ��
        var intersections = Physics.OverlapBox(checkPositions[0], checkExtents, testTransform.rotation, placementMask);

        // �����ϴ� �ݶ��̴��� 2�� �̻��̸� ��ġ �Ұ�
        if (intersections.Length > 1)
        {
            return false;
        }
        else if (intersections.Length == 1)
        {
            // ���� ���� ��ġ���� ������ ���
            if (intersections[0] != collider)
            {
                return false;
            }
        }

        // ��� �𼭸��� ǥ�鿡 �����ִ��� Ȯ��
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
        // ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
        // ��ġ ���� false�� ����
        IsPlaced = false;
    }
}