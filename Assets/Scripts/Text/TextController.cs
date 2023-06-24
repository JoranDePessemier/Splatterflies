using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TextController : MonoBehaviour
{
    [SerializeField]
    RectTransform _backGround;

    [SerializeField]
    RectTransform _textBox;

    [SerializeField]
    private RectTransform _pijltje;

    [SerializeField]
    private float _pijltjeAppearDissapearTime;

    [SerializeField]
    private float _appearDissapearTime;

    [SerializeField]
    private List<TextMeshProUGUI> _texts;

    [SerializeField]
    private float _scrollSpeed;

    private int _currentTextIndex;

    [SerializeField]
    private UnityEvent _characterAppears;

    [SerializeField]
    private UnityEvent _textClicked;




    private float _endAlphaBackground;
    private Controls _controls;
    private bool _isClicked;

    private void Awake()
    {
        Image backImage = _backGround.GetComponent<Image>();

        _endAlphaBackground = backImage.color.a;
        backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, 0);

        _textBox.localScale = Vector3.zero;

        foreach(TextMeshProUGUI text in _texts)
        {
            text.maxVisibleCharacters = 0;
        }

        _controls = new Controls();
        _controls.Enable();
        _controls.PlayerInput.ActionPressed.performed += WasClicked;

        LeanTween.alpha(_pijltje, 0, 0).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);

        Appear();



    }

    private void WasClicked(InputAction.CallbackContext obj)
    {
        _isClicked = true;
    }

    private IEnumerator ScrollText()
    {
        TextMeshProUGUI textObject = _texts[_currentTextIndex];

        for (int i = 0; i < textObject.text.Length; i++)
        {
            textObject.maxVisibleCharacters = i + 1;
            _characterAppears.Invoke();

            if(_isClicked )
            {
                textObject.maxVisibleCharacters = textObject.text.Length - 1;
                _isClicked = false;
                break;
            }


            yield return new WaitForSecondsRealtime(_scrollSpeed);
        }

        ScrollingCompleted();

        while (!_isClicked)
        {
            yield return 0;
        }

        _isClicked = false;

        TextClicked();

    }

    private void TextClicked()
    {
        _textClicked.Invoke();

        LeanTween.alpha(_pijltje, 0, _pijltjeAppearDissapearTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        _texts[_currentTextIndex].maxVisibleCharacters = 0;
        _currentTextIndex++;

        if(_currentTextIndex >= _texts.Count)
        {
            Dissapear();
        }
        else
        {
            StartCoroutine(ScrollText());
        }
    }

    private void ScrollingCompleted()
    {
        LeanTween.alpha(_pijltje, 1, _pijltjeAppearDissapearTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
    }

    public void Appear()
    {
        Time.timeScale = 0;
        LeanTween.alpha(_backGround,_endAlphaBackground ,_appearDissapearTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        LeanTween.scale(_textBox, Vector2.one, _appearDissapearTime).setEase(LeanTweenType.easeOutBack).setOnComplete(AppearCompleted).setIgnoreTimeScale(true); 
    }

    private void AppearCompleted()
    {
        StartCoroutine(ScrollText());
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
