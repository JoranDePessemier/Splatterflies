using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakeButterflyDungeon : BaseButterflyDungeon
{
    private float _currentShakeAmount;

    [SerializeField]
    private float _shakeAmount;

    private void Update()
    {
        if (_isHolding)
        {
            _currentShakeAmount += _controls.PlayerInput.MouseSpeed.ReadValue<Vector2>().magnitude;
        }

        if(_currentShakeAmount >= _shakeAmount)
        {
            Completed();
        }

    }


}
