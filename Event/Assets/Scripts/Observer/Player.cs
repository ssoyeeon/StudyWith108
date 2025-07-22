using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int expToNextLevel = 100;
    public int health = 100;
    public int maxHealth = 100;

    public static event Action<int> OnLevelUp;

    public void GainExperience(int exp)
    {
        experience += exp;
        Debug.Log("����ġ " + exp + "ȹ��! ����: " + experience + "/" + expToNextLevel);

        if(experience >= expToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        experience -= expToNextLevel;
        expToNextLevel += 50;
        maxHealth += 20;
        health = maxHealth;

        Debug.Log("=== ������! ���� ���� : " + level + "===");

        //��� �������鿡�� ������ �˸�
        OnLevelUp?.Invoke(level);
    }
}
