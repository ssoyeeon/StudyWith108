using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마법사 클래스 (자식 클래스)
public class Mage : Character
{
    public int mana = 50;

    void Start()
    {
        characterName = "마법사";
        health = 80;
        attackPower = 15;
    }

    public override void Attack()
    {
        Debug.Log(characterName + "가 마법 공격을 합니다!");
        Debug.Log("마법 데미지: " + (attackPower + 10));
        mana -= 5;
        Debug.Log("마나 소모: 5, 남은 마나: " + mana);
    }

    public override void UseSkill()
    {
        if (mana >= 20)
        {
            Debug.Log(characterName + "가 파이어볼을 시전합니다!");
            Debug.Log("강력한 화염 데미지: " + (attackPower * 2));
            mana -= 20;
            Debug.Log("마나 소모: 20, 남은 마나: " + mana);
        }
        else
        {
            Debug.Log("마나가 부족합니다!");
        }
    }
}
