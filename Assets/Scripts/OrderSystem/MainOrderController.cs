using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyDifficultyEventArgs : EventArgs
{
    public ModifyDifficultyEventArgs(DifficultyModifier currentModifier)
    {
        CurrentModifier = currentModifier;
    }

    public DifficultyModifier CurrentModifier { get; set; }   
}

public class MainOrderController : MonoBehaviour
{
    [SerializeField]
    private DifficultyModifier[] _modifiers;

    [SerializeField]
    private OrderBoardController[] _boards;

    private int _currentDifficultyModifierIndex = -1;
    private DifficultyModifier _currentDifficultyModifier;

    public event EventHandler<ModifyDifficultyEventArgs> ModifyDifficulty;

    private int _nextThreshold;


    private void Start()
    {
        foreach (OrderBoardController board in _boards)
        {
            board.OrderCompleted += Board_OrderCompleted;
        }

        CheckDifficulty();
        ResetBoards();
        GlobalVariables.Instance.CurrentTime = _currentDifficultyModifier.MaxTimePerOrder;
    }

    private void Update()
    {
        GlobalVariables.Instance.CurrentTime -= Time.deltaTime;

        if(GlobalVariables.Instance.CurrentTime < 0)
        {

        }

        print(_nextThreshold);
    }

    private void Board_OrderCompleted(object sender, EventArgs e)
    {
        GlobalVariables.Instance.CompletedOrders++;
        CheckDifficulty();
        ResetBoards();
        GlobalVariables.Instance.CurrentTime = _currentDifficultyModifier.MaxTimePerOrder;
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

            OnModifyDifficulty(new ModifyDifficultyEventArgs(_currentDifficultyModifier));
            
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

    private void OnModifyDifficulty(ModifyDifficultyEventArgs eventArgs)
    {
        var handler = ModifyDifficulty;
        handler?.Invoke(this, eventArgs);
    }
}
