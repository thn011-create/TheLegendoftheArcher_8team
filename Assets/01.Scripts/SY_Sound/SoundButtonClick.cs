using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundButtonClick : MonoBehaviour
{
    // Ŭ�� ȿ����
    public AudioClip clickSound;
    [SerializeField] Button soundBtn; 
    
    private void Awake()
    {
        soundBtn.onClick.AddListener(OnSoundButtonClick);
    }

    
    public void OnSoundButtonClick()
    {
        SoundManager.PlayClip(clickSound);
    }
}
