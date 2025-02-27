using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TypingUI : MonoBehaviour
{
    public Text titelTxt;

    private void Typing()
    {
        titelTxt.DOKill();
        titelTxt.text = "";
        titelTxt.DOText("라스트 투척의 신", 2.0f).SetEase(Ease.Linear)
        .OnComplete(() => ShakeText());
    }
    private void ShakeText()
    {
        titelTxt.transform.DOShakeScale(2f, 0.1f, 5, 90, true)
            .SetLoops(-1, LoopType.Yoyo);
    }
    void Start()
    {
        Typing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
