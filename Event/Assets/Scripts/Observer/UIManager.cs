using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI �Ŵ��� - ������ �� UI ������Ʈ
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

        // �̺�Ʈ ����
        Player.OnLevelUp += OnPlayerLevelUp;

        UpdateUI();
    }

    void OnDestroy()
    {
        // �޸� ���� ������ ���� �̺�Ʈ ���� ����
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("UI �Ŵ���: ������ �˸� ����! �� ����: " + newLevel);

        // UI ������Ʈ
        UpdateUI();

        // ������ �޽��� ǥ��
        if (levelUpMessage != null)
        {
            levelUpMessage.text = "���� " + newLevel + " �޼�!";
            levelUpMessage.gameObject.SetActive(true);

            // 3�� �� �޽��� ����
            Invoke("HideLevelUpMessage", 3f);
        }
    }

    void UpdateUI()
    {
        if (player == null) return;

        if (levelText != null)
            levelText.text = "����: " + player.level;

        if (expText != null)
            expText.text = "����ġ: " + player.experience + "/" + player.expToNextLevel;

        if (healthText != null)
            healthText.text = "ü��: " + player.health + "/" + player.maxHealth;
    }

    void HideLevelUpMessage()
    {
        if (levelUpMessage != null)
            levelUpMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateUI(); // �ǽð� UI ������Ʈ
    }
}
