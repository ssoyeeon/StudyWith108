using UnityEngine;

[RequireComponent(typeof(MeshFilter))]       // �޽� ���� �ʼ�      //��װ� �� �� ��ũ��Ʈ�� �־�� �Ѵ�!!
[RequireComponent(typeof(MeshRenderer))]     // �޽� ������ �ʼ�
[RequireComponent(typeof(Rigidbody))]        // ������ٵ� �ʼ�
[RequireComponent(typeof(Collider))]         // �ݶ��̴� �ʼ�
public class PortalableObject : MonoBehaviour
{
    private GameObject cloneObject;          // ���п��� ���̴� ���� ������Ʈ
    private int inPortalCount = 0;           // ���� ������ ���� �� ī��Ʈ

    private Portal inPortal;                 // ������ ����
    private Portal outPortal;                // ������ ����
    private new Rigidbody rigidbody;        // ĳ�̵� ������ٵ�
    protected new Collider collider;         // ĳ�̵� �ݶ��̴�

    // ���� ����� 180�� ȸ���� ���� ���
    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    protected virtual void Awake()     //�̰� ���߿� �ٸ� ��ũ��Ʈ���� ����� �޾��� �� �������̵�� �缱�� ����
    {
        // ���� ������Ʈ ���� �� ����
        cloneObject = new GameObject();
        cloneObject.SetActive(false);

        // �޽� ���Ϳ� ������ ����
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        // ������Ʈ ĳ��
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        // ������ ������ ������Ʈ �ʿ� ����
        if (inPortal == null || outPortal == null)
        {
            return;
        }

        // ���� ������Ʈ�� Ȱ��ȭ �����̰� ���� ������ ��ġ�� ���
        if (cloneObject.activeSelf && inPortal.IsPlaced && outPortal.IsPlaced)
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            // ���� �������� ���� ������Ʈ ��ġ ���
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            // ���� �������� ���� ������Ʈ ȸ�� ���
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;
        }
        else
        {
            // ������ ���ų� ��Ȱ��ȭ ���¸� ���� ������Ʈ�� �ָ� �̵�
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        // ������ ���� ���� ����
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        // ������ ��ġ�� ������ �浹 ����
        Physics.IgnoreCollision(collider, wallCollider);

        // ���� ������Ʈ ��Ȱ��ȭ
        cloneObject.SetActive(false);

        // ���� ���� ī��Ʈ ����
        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        // ������ �浹 �ٽ� Ȱ��ȭ
        Physics.IgnoreCollision(collider, wallCollider, false);

        // ���� ���� ī��Ʈ ����
        --inPortalCount;

        // ��� ���п��� �������� ���� ������Ʈ ��Ȱ��ȭ
        if (inPortalCount == 0)
        {
            cloneObject.SetActive(false);
        }
    }

    public virtual void Warp()
    {
        var inTransform = inPortal.transform;
        var outTransform = outPortal.transform;

        // ������Ʈ ��ġ ��ȯ
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
        relativePos = halfTurn * relativePos;
        transform.position = outTransform.TransformPoint(relativePos);

        // ������Ʈ ȸ�� ��ȯ
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = halfTurn * relativeRot;
        transform.rotation = outTransform.rotation * relativeRot;

        // ������ٵ� �ӵ� ��ȯ
        Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);
        relativeVel = halfTurn * relativeVel;
        rigidbody.velocity = outTransform.TransformDirection(relativeVel);

        // �Ա�/�ⱸ ���� ���� ��ü
        var tmp = inPortal;
        inPortal = outPortal;
        outPortal = tmp;
    }
}