using UnityEngine;

public class PortalPair : MonoBehaviour
{
    public Portal[] Portals { private set; get; }

    private void Awake()
    {
        // 자식 오브젝트에서 Portal 컴포넌트 가져오기
        Portals = GetComponentsInChildren<Portal>();

        // 정확히 2개의 포털이 있는지 체크
        if (Portals.Length != 2)
        {
            Debug.LogError("포탈이 2개만 있어야 합니다. ");
        }
    }
}