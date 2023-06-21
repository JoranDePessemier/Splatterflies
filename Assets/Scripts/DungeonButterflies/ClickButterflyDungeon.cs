using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButterflyDungeon : BaseButterflyDungeon
{
    private int _clickCounter;

    [SerializeField]
    private int _amountToClick;

    protected override void Clicked()
    {
        base.Clicked();
        _clickCounter++;

        if(_clickCounter >= _amountToClick)
        {
            Completed();
        }
    }
}
