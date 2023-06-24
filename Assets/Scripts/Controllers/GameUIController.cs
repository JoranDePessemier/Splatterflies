using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentTimeText;

    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    [SerializeField]
    private RectTransform _timerTransform;

    [SerializeField]
    private float _timerMoveUpAmount;

    [SerializeField]
    private float _timerMoveSpeed;

    private bool _timerIsThere = true;

    private void Awake()
    {
        Vector3 movePosition = new Vector3(0, _timerMoveUpAmount, 0);
        LeanTween.move(_timerTransform, movePosition, 0).setEase(LeanTweenType.easeInCubic);
        TimerAppears();
    }


    private void Update()
    {
        _currentScoreText.text = GlobalVariables.Instance.CompletedOrders.ToString();

        _currentTimeText.text = ((int)Mathf.Clamp(GlobalVariables.Instance.CurrentTime,0,int.MaxValue)).ToString();



        if(GlobalVariables.Instance.CurrentTime < 0 && _timerIsThere)
        {
            TimerDissapears();
            _timerIsThere = false;
        }
    }


    public void TimerAppears()
    {

        LeanTween.move(_timerTransform, Vector2.zero, _timerMoveSpeed).setEase(LeanTweenType.easeOutCubic);
    }

    private void TimerDissapears()
    {
        Vector3 movePosition = new Vector3(0,_timerMoveUpAmount,0);

        LeanTween.move(_timerTransform, movePosition, _timerMoveSpeed).setEase(LeanTweenType.easeInCubic);
    }
}
