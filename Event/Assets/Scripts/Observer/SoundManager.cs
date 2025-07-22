using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ŵ��� - ������ �� ���� ���
public class SoundManager : MonoBehaviour, ILevelUpObserver
{
    public AudioClip levelUpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // �̺�Ʈ ����
        Player.OnLevelUp += OnPlayerLevelUp;
    }

    void OnDestroy()
    {
        Player.OnLevelUp -= OnPlayerLevelUp;
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        Debug.Log("���� �Ŵ���: ������ ���� ���! ����: " + newLevel);

        if (levelUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }
        else
        {
            // ���尡 ��� �α׷� Ȯ��
            Debug.Log("������ ���� ���!");
        }
    }
}
