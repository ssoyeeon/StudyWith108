using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �������̽� - �̺�Ʈ�� ���� �� �ִ� ��ü���� �����ؾ� ��
public interface ILevelUpObserver
{
    void OnPlayerLevelUp(int newLevel);
}
