using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualOverrideGameManager : MonoBehaviour
{
    public Button warriorButton;
    public Button mageButton;
    public Button rogueButton;
    public Button attackButton;
    public Button skillButton;
    public Text infoText;

    private Character currentCharacter;

    void Start()
    {
        // ��ư �̺�Ʈ ����
        warriorButton.onClick.AddListener(() => SelectCharacter("Warrior"));
        mageButton.onClick.AddListener(() => SelectCharacter("Mage"));
        rogueButton.onClick.AddListener(() => SelectCharacter("Rogue"));

        attackButton.onClick.AddListener(() =>
        {
            if (currentCharacter != null) currentCharacter.Attack();
        });

        skillButton.onClick.AddListener(() =>
        {
            if (currentCharacter != null) currentCharacter.UseSkill();
        });
    }

    void SelectCharacter(string characterType)
    {
        // ���� ĳ���� ����
        if (currentCharacter != null)
        {
            Destroy(currentCharacter.gameObject);
        }

        // �� ĳ���� ����
        GameObject newCharacterObj = new GameObject(characterType);

        switch (characterType)
        {
            case "Warrior":
                currentCharacter = newCharacterObj.AddComponent<Warrior>();
                break;
            case "Mage":
                currentCharacter = newCharacterObj.AddComponent<Mage>();
                break;
            case "Rogue":
                currentCharacter = newCharacterObj.AddComponent<Rogue>();
                break;
        }

        Debug.Log("=== " + characterType + " ���õ� ===");
        ShowCharacterInfo();
    }

    void ShowCharacterInfo()
    {
        Debug.Log("ĳ����: " + currentCharacter.characterName);
        Debug.Log("ü��: " + currentCharacter.health);
        Debug.Log("���ݷ�: " + currentCharacter.attackPower);
    }

    // �׽�Ʈ�� Ű���� �Է�
    void Update()
    {
        if (currentCharacter == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCharacter.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentCharacter.UseSkill();
        }
    }
}