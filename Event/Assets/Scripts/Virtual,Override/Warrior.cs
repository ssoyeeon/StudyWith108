using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전사 클래스 (자식 클래스)
public class Warrior : Character
{
    public int shieldDefense = 5;

    void Start()
    {
        characterName = "전사";
        health = 150;
        attackPower = 20;
    }

    // override: 부모의 메서드를 재정의
    public override void Attack()
    {
        // base: 부모 클래스의 원래 메서드 호출
        base.Attack();
        Debug.Log("검으로 강력하게 내려칩니다!");
    }

    public override void UseSkill()
    {
        Debug.Log(characterName + "가 방패막기를 사용합니다!");
        Debug.Log("방어력이 " + shieldDefense + " 증가했습니다!");
    }
}