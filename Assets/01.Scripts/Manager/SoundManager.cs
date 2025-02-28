using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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

    public  AudioClip[] bgmCilps; // 씬별 bgm
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

        // scene 변경되는지 사운드매니저에서 확인
        SceneManager.sceneLoaded += OnSceneLoaded; 

    }

    // 파괴
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.buildIndex);
    }
    

    public void PlayBGM(int sceneIdx)
    {
        if (musicAudioSource.clip == bgmCilps[sceneIdx]) return; // 중복 재생 방지

        musicAudioSource.Stop(); // 기존 BGM 정지
        musicAudioSource.clip = bgmCilps[sceneIdx];
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }


    // 볼륨 //

    // bgm 볼륨 저장
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    // sfx 볼륨 저장
    public void SetSFXVolume(float volume)
    {
        soundEffectVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
