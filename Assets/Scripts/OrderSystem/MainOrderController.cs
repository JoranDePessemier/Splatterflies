using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOrderController : MonoBehaviour
{
    [SerializeField]
    private DifficultyModifier[] _modifiers;

    [SerializeField]
    private OrderBoardController[] _boards;

    private int _currentDifficultyModifierIndex = -1;
    private DifficultyModifier _currentDifficultyModifier;

    private int _nextThreshold;



    private void Start()
    {
        foreach (OrderBoardController board in _boards)
        {
            board.OrderCompleted += Board_OrderCompleted;
        }

        CheckDifficulty();
        ResetBoards();
    }

    private void Board_OrderCompleted(object sender, EventArgs e)
    {
        GlobalVariables.Instance.CompletedOrders++;
        CheckDifficulty();
        ResetBoards();
    }

    private void ResetBoards()
    {
        for (int i = 0; i < _currentDifficultyModifier.OrderAmount; i++)
        {
            if (!_boards[i].HasActiveOrder)
            {
                _boards[i].GenerateOrder(UnityEngine.Random.Range(1, _currentDifficultyModifier.MaxButterflyAmount + 1), _currentDifficultyModifier.ButterflyTypeAmount);
            }
        }
    }

    private void CheckDifficulty()
    {
        if( _nextThreshold >= 0 && GlobalVariables.Instance.CompletedOrders >= _nextThreshold)
        {
            _currentDifficultyModifierIndex++;
            _currentDifficultyModifier = _modifiers[_currentDifficultyModifierIndex];
            
            if(_currentDifficultyModifierIndex + 1 < _modifiers.Length)
            {
                _nextThreshold = _modifiers[_currentDifficultyModifierIndex + 1].ModifyMinScore;
            }
            else
            {
                _nextThreshold = -1;
            }
        }
    }
}
