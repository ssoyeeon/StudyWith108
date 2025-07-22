using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Ŭ���� (�ڽ� Ŭ����)
public class Rogue : Character
{
    public int criticalChance = 30;

    void Start()
    {
        characterName = "����";
        health = 90;
        attackPower = 12;
    }

    public override void Attack()
    {
        Debug.Log(characterName + "�� �����ϰ� �����մϴ�!");

        // 30% Ȯ���� ġ��Ÿ
        if (Random.Range(0, 100) < criticalChance)
        {
            Debug.Log("ġ��Ÿ! ������: " + (attackPower * 2));
        }
        else
        {
            Debug.Log("�Ϲ� ������: " + attackPower);
        }
    }

    public override void UseSkill()
    {
        Debug.Log(characterName + "�� ������ ����մϴ�!");
        Debug.Log("���� ������ ġ��Ÿ Ȯ���� 100%�� �˴ϴ�!");
        criticalChance = 100;
    }
}

