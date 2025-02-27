using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // ����,��ġ ����
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    //// ����� �ͼ� - ���� ���� 
    //[SerializeField] private AudioMixer audioMixer;
    
    //// ����� Ŭ�� ��� �迭
    //[SerializeField] AudioClip[] bgms;
    //[SerializeField] AudioClip[] sfxs;

    //// �÷��� ���� ����� �ҽ�
    //[SerializeField] AudioSource playbgm;
    //[SerializeField] AudioSource playsfx;

    public  AudioClip musicCilp;
    private AudioSource musicAudioSource;

    // ����ҽ� ������
    public SoundSource soundSourcePrefab;

    public enum SoundType
    {
        BGM,
        SFX
    }

    // ���� : ������ ����� �Ҵ��� Ŭ���� ������ ���ƾ� ��
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

        // bgm ����,����
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
        if (musicAudioSource.clip == clip) return; // �ߺ���� ����

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

    // ���� //

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
