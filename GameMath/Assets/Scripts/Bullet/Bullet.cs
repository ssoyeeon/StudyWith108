using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float radius = 5f; // 회전 반지름
    public float speed = 2f;  // 회전 속도
    private Vector3 centerPoint; // 중심점
    private float angle = 0f;

    void Start()
    {
        centerPoint = transform.position;
    }

    void Update()
    {
        // 원 궤도 이동
        angle += speed * Time.deltaTime;
        float x = centerPoint.x + radius * Mathf.Cos(angle);
        float y = centerPoint.y + radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
