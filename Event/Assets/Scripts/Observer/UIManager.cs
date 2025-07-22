using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 매니저 - 레벨업 시 UI 업데이트
public class UIManager : MonoBehaviour, ILevelUpObserver
{
    public Text levelText;
    public Text expText;
    public Text healthText;
    public Text levelUpMessage;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        // 이벤트 구독
        Player.OnLevelUp += OnPlayerLevelUp;

        UpdateUI();
    }

    void OnDestroy()
    {
        // 메모리 누수 방지를 위한 이벤트 구독 해제
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("UI 매니저: 레벨업 알림 받음! 새 레벨: " + newLevel);

        // UI 업데이트
        UpdateUI();

        // 레벨업 메시지 표시
        if (levelUpMessage != null)
        {
            levelUpMessage.text = "레벨 " + newLevel + " 달성!";
            levelUpMessage.gameObject.SetActive(true);

            // 3초 후 메시지 숨김
            Invoke("HideLevelUpMessage", 3f);
        }
    }

    void UpdateUI()
    {
        if (player == null) return;

        if (levelText != null)
            levelText.text = "레벨: " + player.level;

        if (expText != null)
            expText.text = "경험치: " + player.experience + "/" + player.expToNextLevel;

        if (healthText != null)
            healthText.text = "체력: " + player.health + "/" + player.maxHealth;
    }

    void HideLevelUpMessage()
    {
        if (levelUpMessage != null)
            levelUpMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateUI(); // 실시간 UI 업데이트
    }
}
