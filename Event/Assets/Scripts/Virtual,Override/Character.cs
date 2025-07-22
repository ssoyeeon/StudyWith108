using UnityEngine;
using UnityEngine.UI;

// �⺻ ĳ���� Ŭ���� (�θ� Ŭ����)
public class Character : MonoBehaviour
{
    public string characterName;
    public int health = 100;
    public int attackPower = 10;

    // virtual Ű����: �ڽ� Ŭ�������� ������ ����
    public virtual void Attack()
    {
        Debug.Log(characterName + "�� �⺻ ������ �մϴ�!");
        Debug.Log("������: " + attackPower);
    }

    public virtual void UseSkill()
    {
        Debug.Log(characterName + "�� �⺻ ��ų�� ����մϴ�!");
    }

    // ���� �޼��� (��� ĳ���Ͱ� ���� ������� ���)
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(characterName + "�� " + damage + " �������� �޾ҽ��ϴ�. ���� ü��: " + health);
    }
}
