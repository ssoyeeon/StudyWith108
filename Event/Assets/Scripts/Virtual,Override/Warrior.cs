using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� Ŭ���� (�ڽ� Ŭ����)
public class Warrior : Character
{
    public int shieldDefense = 5;

    void Start()
    {
        characterName = "����";
        health = 150;
        attackPower = 20;
    }

    // override: �θ��� �޼��带 ������
    public override void Attack()
    {
        // base: �θ� Ŭ������ ���� �޼��� ȣ��
        base.Attack();
        Debug.Log("������ �����ϰ� ����Ĩ�ϴ�!");
    }

    public override void UseSkill()
    {
        Debug.Log(characterName + "�� ���и��⸦ ����մϴ�!");
        Debug.Log("������ " + shieldDefense + " �����߽��ϴ�!");
    }
}