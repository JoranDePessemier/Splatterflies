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

    [SerializeField]
    private int _timerWarningSeconds;

    private bool _timerIsThere = false;
    private Animator _timerAnimator;

    private float _previousTime;

    private void Awake()
    {
        Vector3 movePosition = new Vector3(0, _timerMoveUpAmount, 0);
        LeanTween.move(_timerTransform, movePosition, 0).setEase(LeanTweenType.easeInCubic);
        _timerAnimator = _currentTimeText.GetComponent<Animator>();
    }


    private void Update()
    {
        _currentScoreText.text = GlobalVariables.Instance.CompletedOrders.ToString();

        _currentTimeText.text = ((int)Mathf.Clamp(GlobalVariables.Instance.CurrentTime,0,int.MaxValue)).ToString();

        if(GlobalVariables.Instance.CurrentTime <= _timerWarningSeconds)
        {
            _timerAnimator.SetBool("TimeLow", true);
        }
        else
        {
            _timerAnimator.SetBool("TimeLow", false);
        }

        if(Mathf.Abs(GlobalVariables.Instance.CurrentTime - _previousTime) > 2)
        {
            _timerAnimator.SetTrigger("GainTime");
        }


        if(GlobalVariables.Instance.CurrentTime < 0 && _timerIsThere || (!GlobalVariables.Instance.EndlessMode && GlobalVariables.Instance.GameComplete && _timerIsThere))
        {
            TimerDissapears();
            _timerIsThere = false;
        }
        else if (GlobalVariables.Instance.CurrentTime >= 0  && GlobalVariables.Instance.TimerStarted && !_timerIsThere && !GlobalVariables.Instance.GameComplete)
        {
            _timerIsThere = true;
            TimerAppears();
        }

        _previousTime = GlobalVariables.Instance.CurrentTime;
    }


    private void TimerAppears()
    {
        LeanTween.move(_timerTransform, Vector2.zero, _timerMoveSpeed).setEase(LeanTweenType.easeOutCubic);
    }

    private void TimerDissapears()
    {
        Vector3 movePosition = new Vector3(0,_timerMoveUpAmount,0);

        LeanTween.move(_timerTransform, movePosition, _timerMoveSpeed).setEase(LeanTweenType.easeInCubic);
    }
}
