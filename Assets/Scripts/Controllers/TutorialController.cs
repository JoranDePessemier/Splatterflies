using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private CameraController _camController;

    [SerializeField]
    private TextController _startingText;

    [SerializeField]
    private TextController _goToDungeonText;

    [SerializeField]
    private TextController _InDungeonText;

    [SerializeField]
    private int _startTimerScore;

    private bool _hasGoneToDungeon;

    private void Awake()
    {
        _camController = FindObjectOfType<CameraController>();
        _camController.EnableDownMovement = false;
        _camController.WentUpAfterStart += FirstTextAppear;
        GlobalVariables.Instance.TimerStarted = false;
    }

    private void FirstTextAppear(object sender, EventArgs e)
    {
        _startingText.Appear();
    }

    private void Update()
    {
        if(_camController.EnableDownMovement == false && FindObjectsOfType<BaseButterflyFlying>().Length <= 0)
        {
            _camController.EnableDownMovement = true;
            _goToDungeonText.Appear();
        }
        else if (!_hasGoneToDungeon && GlobalVariables.Instance.ScreenState == ScreenType.Bottom)
        {
            _hasGoneToDungeon = true;
            _InDungeonText.Appear();
        }
        else if(GlobalVariables.Instance.CompletedOrders >= _startTimerScore)
        {
            GlobalVariables.Instance.TimerStarted = true;
        }
    }
}
