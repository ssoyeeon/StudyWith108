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
        // 버튼 이벤트 연결
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
        // 기존 캐릭터 삭제
        if (currentCharacter != null)
        {
            Destroy(currentCharacter.gameObject);
        }

        // 새 캐릭터 생성
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

        Debug.Log("=== " + characterType + " 선택됨 ===");
        ShowCharacterInfo();
    }

    void ShowCharacterInfo()
    {
        Debug.Log("캐릭터: " + currentCharacter.characterName);
        Debug.Log("체력: " + currentCharacter.health);
        Debug.Log("공격력: " + currentCharacter.attackPower);
    }

    // 테스트용 키보드 입력
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