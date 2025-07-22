using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ �Ŵ��� - ������ �� ��ƼŬ ȿ��
public class EffectManager : MonoBehaviour, ILevelUpObserver
{
    public ParticleSystem levelUpEffect;
    public Color[] levelColors; // ������ ����

    void Start()
    {
        Player.OnLevelUp += OnPlayerLevelUp;

        // �⺻ ���� ����
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
        Debug.Log("����Ʈ �Ŵ���: ������ ȿ�� ���! ����: " + newLevel);

        if (levelUpEffect != null)
        {
            // ������ ���� ���� ����
            var main = levelUpEffect.main;
            main.startColor = levelColors[(newLevel - 1) % levelColors.Length];

            levelUpEffect.Play();
        }
        else
        {
            Debug.Log("������ ����Ʈ ���!");
        }
    }
}
