using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ �Ŵ��� - ������ �� �� ����Ʈ ����
public class QuestManager : MonoBehaviour, ILevelUpObserver
{
    public string[] levelQuests = {
        "ù ��° ������ �����ϼ���!",
        "������ 10������ óġ�ϼ���",
        "������ �����ϼ���",
        "���� ���Ϳ� �ο켼��",
        "������ ���⸦ ã������"
    };

    void Start()
    {
        Player.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDestroy()
    {
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("����Ʈ �Ŵ���: �� ����Ʈ ����! ����: " + newLevel);

        if (newLevel <= levelQuests.Length)
        {
            string newQuest = levelQuests[newLevel - 1];
            Debug.Log("�� ����Ʈ: " + newQuest);
        }
    }
}
