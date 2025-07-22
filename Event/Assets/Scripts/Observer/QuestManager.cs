using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퀘스트 매니저 - 레벨업 시 새 퀘스트 해제
public class QuestManager : MonoBehaviour, ILevelUpObserver
{
    public string[] levelQuests = {
        "첫 번째 모험을 시작하세요!",
        "슬라임 10마리를 처치하세요",
        "던전에 도전하세요",
        "보스 몬스터와 싸우세요",
        "전설의 무기를 찾으세요"
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
        Debug.Log("퀘스트 매니저: 새 퀘스트 해제! 레벨: " + newLevel);

        if (newLevel <= levelQuests.Length)
        {
            string newQuest = levelQuests[newLevel - 1];
            Debug.Log("새 퀘스트: " + newQuest);
        }
    }
}
