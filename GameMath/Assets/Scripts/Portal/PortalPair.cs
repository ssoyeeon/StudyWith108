using UnityEngine;

public class PortalPair : MonoBehaviour
{
    public Portal[] Portals { private set; get; }

    private void Awake()
    {
        // �ڽ� ������Ʈ���� Portal ������Ʈ ��������
        Portals = GetComponentsInChildren<Portal>();

        // ��Ȯ�� 2���� ������ �ִ��� üũ
        if (Portals.Length != 2)
        {
            Debug.LogError("��Ż�� 2���� �־�� �մϴ�. ");
        }
    }
}