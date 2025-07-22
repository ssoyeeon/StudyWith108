using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 도적 클래스 (자식 클래스)
public class Rogue : Character
{
    public int criticalChance = 30;

    void Start()
    {
        characterName = "도적";
        health = 90;
        attackPower = 12;
    }

    public override void Attack()
    {
        Debug.Log(characterName + "가 은밀하게 공격합니다!");

        // 30% 확률로 치명타
        if (Random.Range(0, 100) < criticalChance)
        {
            Debug.Log("치명타! 데미지: " + (attackPower * 2));
        }
        else
        {
            Debug.Log("일반 데미지: " + attackPower);
        }
    }

    public override void UseSkill()
    {
        Debug.Log(characterName + "가 은신을 사용합니다!");
        Debug.Log("다음 공격의 치명타 확률이 100%가 됩니다!");
        criticalChance = 100;
    }
}

