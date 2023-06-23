using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyPulse : MonoBehaviour
{
    [SerializeField]
    private Vector2 _maxScale;

    [SerializeField]
    private Vector2 _minMaxTime;

    [SerializeField]
    private GameObject _sprite;

    private float _time;

    private void Awake()
    {
        _time = UnityEngine.Random.Range(_minMaxTime.x,_minMaxTime.y);
        StartPulsing();
    }

    private void StartPulsing()
    {
        LeanTween.scale(_sprite, _maxScale, _time).setEase(LeanTweenType.easeInOutSine).setOnComplete(RedoPulsing);
    }

    private void RedoPulsing()
    {
        LeanTween.scale(_sprite, Vector2.one, _time).setEase(LeanTweenType.easeInOutSine).setOnComplete(StartPulsing);
    }
}
