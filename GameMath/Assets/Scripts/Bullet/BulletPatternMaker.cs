using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPatternMaker : MonoBehaviour
{
    public float patternSpeed = 2f;             //���� ȸ�� �ӵ�
    public float bulletSpeed = 5f;              //�Ѿ� �ӵ�
    public int bulletAmount = 10;               //�Ѿ��� ����
    public float radius = 2f;                   //���� ������

    public GameObject bulletPrefab;             //�Ѿ� ������
    private float timer;                        //UI�� ����� Ÿ�̸�
    private bool patternStarted = false;        //������ ���۵ƴ���
    public Text timerText;                      //UI �ؽ�Ʈ�� ��﷡��
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
