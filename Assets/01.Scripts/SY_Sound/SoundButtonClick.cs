using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundButtonClick : MonoBehaviour
{
    // 클릭 효과음
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
