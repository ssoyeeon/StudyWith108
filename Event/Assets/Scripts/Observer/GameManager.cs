using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Button expButton;
    public Button bigExpButton;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (expButton != null)
            expButton.onClick.AddListener(() => player.GainExperience(30));

        if (bigExpButton != null)
            bigExpButton.onClick.AddListener(() => player.GainExperience(100));
    }

    void Update()
    {
        // 테스트용 키보드 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.GainExperience(30);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player.GainExperience(100);
        }
    }
}
