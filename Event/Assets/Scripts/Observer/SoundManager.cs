using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사운드 매니저 - 레벨업 시 사운드 재생
public class SoundManager : MonoBehaviour, ILevelUpObserver
{
    public AudioClip levelUpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // 이벤트 구독
        Player.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDestroy()
    {
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("사운드 매니저: 레벨업 사운드 재생! 레벨: " + newLevel);

        if (levelUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }
        else
        {
            // 사운드가 없어도 로그로 확인
            Debug.Log("레벨업 사운드 재생!");
        }
    }
}
