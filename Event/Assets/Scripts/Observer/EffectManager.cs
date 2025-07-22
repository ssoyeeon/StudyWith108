using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트 매니저 - 레벨업 시 파티클 효과
public class EffectManager : MonoBehaviour, ILevelUpObserver
{
    public ParticleSystem levelUpEffect;
    public Color[] levelColors; // 레벨별 색상

    void Start()
    {
        Player.OnLevelUp += OnPlayerLevelUp;

        // 기본 색상 설정
        if (levelColors == null || levelColors.Length == 0)
        {
            levelColors = new Color[] { Color.yellow, Color.green, Color.blue, Color.red, Color.magenta };
        }
    }

    void OnDestroy()
    {
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("이펙트 매니저: 레벨업 효과 재생! 레벨: " + newLevel);

        if (levelUpEffect != null)
        {
            // 레벨에 따른 색상 변경
            var main = levelUpEffect.main;
            main.startColor = levelColors[(newLevel - 1) % levelColors.Length];

            levelUpEffect.Play();
        }
        else
        {
            Debug.Log("레벨업 이펙트 재생!");
        }
    }
}
