using UnityEngine;
using UnityEngine.UI;

// 기본 캐릭터 클래스 (부모 클래스)
public class Character : MonoBehaviour
{
    public string characterName;
    public int health = 100;
    public int attackPower = 10;

    // virtual 키워드: 자식 클래스에서 재정의 가능
    public virtual void Attack()
    {
        Debug.Log(characterName + "가 기본 공격을 합니다!");
        Debug.Log("데미지: " + attackPower);
    }

    public virtual void UseSkill()
    {
        Debug.Log(characterName + "가 기본 스킬을 사용합니다!");
    }

    // 공통 메서드 (모든 캐릭터가 같은 방식으로 사용)
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(characterName + "가 " + damage + " 데미지를 받았습니다. 남은 체력: " + health);
    }
}
