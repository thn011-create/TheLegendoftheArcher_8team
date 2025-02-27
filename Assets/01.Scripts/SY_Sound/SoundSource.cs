using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    // 오디오 소스 재생
    public void Play(AudioClip clip, float sfxVolume, float sfxPitchVariance)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        CancelInvoke();

        audioSource.clip = clip;
        audioSource.volume = sfxVolume;
        audioSource.Play();
        audioSource.pitch = 1f + Random.Range(-sfxPitchVariance, sfxPitchVariance);

        Invoke("Disable", clip.length + 2);
    }
    public void Disable()
    {
        audioSource.Stop();
        Destroy(this.gameObject);
    }
}
