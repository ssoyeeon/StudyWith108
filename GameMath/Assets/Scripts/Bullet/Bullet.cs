using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float radius = 5f; // ȸ�� ������
    public float speed = 2f;  // ȸ�� �ӵ�
    private Vector3 centerPoint; // �߽���
    private float angle = 0f;

    void Start()
    {
        centerPoint = transform.position;
    }

    void Update()
    {
        // �� �˵� �̵�
        angle += speed * Time.deltaTime;
        float x = centerPoint.x + radius * Mathf.Cos(angle);
        float y = centerPoint.y + radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
