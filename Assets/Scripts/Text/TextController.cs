using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField]
    RectTransform _backGround;

    [SerializeField]
    RectTransform _textBox;

    [SerializeField]
    private float _appearDissapearTime;

    private float _endAlphaBackground;

    private void Awake()
    {
        Image backImage = _backGround.GetComponent<Image>();

        _endAlphaBackground = backImage.color.a;
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, 0);

        _textBox.localScale = Vector3.zero;

        Appear();

    }

    public void Appear()
    {
        Time.timeScale = 0;
        LeanTween.alpha(_backGround,_endAlphaBackground ,_appearDissapearTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        LeanTween.scale(_textBox, Vector2.one, _appearDissapearTime).setEase(LeanTweenType.easeOutBack).setOnComplete(AppearCompleted).setIgnoreTimeScale(true); 
    }

    private void AppearCompleted()
    {
        Dissapear();
    }

    private void Dissapear()
    {
        LeanTween.alpha(_backGround, 0, _appearDissapearTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        LeanTween.scale(_textBox, Vector2.zero, _appearDissapearTime).setEase(LeanTweenType.easeInBack).setOnComplete(DissapearCompleted).setIgnoreTimeScale(true);
    }

    private void DissapearCompleted()
    {
        Time.timeScale = 1;
    }
}
