using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // 볼륨,피치 조절
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    //// 오디오 믹서 - 사운드 조절 
    //[SerializeField] private AudioMixer audioMixer;
    
    //// 오디오 클립 담는 배열
    //[SerializeField] AudioClip[] bgms;
    //[SerializeField] AudioClip[] sfxs;

    //// 플레이 중인 오디오 소스
    //[SerializeField] AudioSource playbgm;
    //[SerializeField] AudioSource playsfx;

    public  AudioClip musicCilp;
    private AudioSource musicAudioSource;

    // 사운드소스 프리팹
    public SoundSource soundSourcePrefab;

    public enum SoundType
    {
        BGM,
        SFX
    }

    // 주의 : 정의한 내용과 할당한 클립의 순서가 같아야 함
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

        // bgm 볼륨,루프
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        musicAudioSource.loop = true;

    }

    private void Start()
    {
        ChangeBackGroundMusic(musicCilp);
    }

    
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        if (musicAudioSource.clip == clip) return; // 중복재생 방지

        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    // 볼륨 //

    // bgm 
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    // sfx
    public void SetSFXVolume(float volume)
    {
        soundEffectVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
