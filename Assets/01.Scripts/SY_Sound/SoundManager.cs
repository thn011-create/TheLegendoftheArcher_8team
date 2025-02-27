using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // audio clip ��� �迭
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    // �÷����ϴ� audio source
    [SerializeField] AudioSource playbgm;
    [SerializeField] AudioSource playsfx;

    public enum Bgm
    {
        // BGM �� 3���� ����
    }

    public enum Sfx
    {
        // SFX ����
    }

    private void Awake()
    {
        // �̱���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {  Destroy(gameObject); }
    }
    
    // bgm ��� (enum Bgm ������)
    public void PlayBGM(Bgm bgmIdx)
    {

    }

    // sfx ��� (enum Sfx ������)
    public void PlaySFX(Sfx sfxIdx)
    {
        // PLAY ONESHOT �̿�
    }

    // bgm ����
    public void StopBGM()
    {
        playbgm.Stop();
    }
}
