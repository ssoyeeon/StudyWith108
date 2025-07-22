using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ Ŭ���� (�ڽ� Ŭ����)
public class Mage : Character
{
    public int mana = 50;

    void Start()
    {
        characterName = "������";
        health = 80;
        attackPower = 15;
    }

    public override void Attack()
    {
        Debug.Log(characterName + "�� ���� ������ �մϴ�!");
        Debug.Log("���� ������: " + (attackPower + 10));
        mana -= 5;
        Debug.Log("���� �Ҹ�: 5, ���� ����: " + mana);
    }

    public override void UseSkill()
    {
        if (mana >= 20)
        {
            Debug.Log(characterName + "�� ���̾�� �����մϴ�!");
            Debug.Log("������ ȭ�� ������: " + (attackPower * 2));
            mana -= 20;
            Debug.Log("���� �Ҹ�: 20, ���� ����: " + mana);
        }
        else
        {
            Debug.Log("������ �����մϴ�!");
        }
    }
}
