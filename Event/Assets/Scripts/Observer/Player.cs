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
        Debug.Log("경험치 " + exp + "획득! 현재: " + experience + "/" + expToNextLevel);

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

        Debug.Log("=== 레벨업! 현재 레벨 : " + level + "===");

        //모든 옵저버들에게 레벨업 알림
        OnLevelUp?.Invoke(level);
    }
}
