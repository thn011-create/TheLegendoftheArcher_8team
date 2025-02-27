using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // audio clip 담는 배열
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    // 플레이하는 audio source
    [SerializeField] AudioSource playbgm;
    [SerializeField] AudioSource playsfx;

    public enum Bgm
    {
        // BGM 총 3가지 예상
    }

    public enum Sfx
    {
        // SFX 종류
    }

    private void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {  Destroy(gameObject); }
    }
    
    // bgm 재생 (enum Bgm 열거형)
    public void PlayBGM(Bgm bgmIdx)
    {

    }

    // sfx 재생 (enum Sfx 열거형)
    public void PlaySFX(Sfx sfxIdx)
    {
        // PLAY ONESHOT 이용
    }

    // bgm 정지
    public void StopBGM()
    {
        playbgm.Stop();
    }
}
