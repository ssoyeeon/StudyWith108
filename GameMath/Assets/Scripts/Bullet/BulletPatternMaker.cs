using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPatternMaker : MonoBehaviour
{
    public float patternSpeed = 2f;             //패턴 회전 속도
    public float bulletSpeed = 5f;              //총알 속도
    public int bulletAmount = 10;               //총알의 갯수
    public float radius = 2f;                   //패턴 반지름

    public GameObject bulletPrefab;             //총알 프리팹
    private float timer;                        //UI에 띄워줄 타이머
    private bool patternStarted = false;        //패턴이 시작됐는지
    public Text timerText;                      //UI 텍스트로 띄울래용
    private float angle;

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(patternStarted == false)
            {
                patternStarted = true;

                GameObject bullet = Instantiate(bulletPrefab , transform.position, Quaternion.identity);
            }
            else
            {
                patternStarted = false;
            }
        }
    }
}
