using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private UnityEvent _gameOverEvent;

    [SerializeField]
    private UnityEvent _gameCompleteEvent;

    [SerializeField]
    private string _mainMenuScene;

    [SerializeField]
    [Tooltip("Only applicable if the game is in story mode!")]
    private int _completeGameThreshold;

    private int _currentDifficultyModifierIndex = -1;
    private DifficultyModifier _currentDifficultyModifier;

    public event EventHandler<ModifyDifficultyEventArgs> ModifyDifficulty;

    private int _nextThreshold;

    private CameraController _cameraController;

    private bool _gameOver;


    private void Start()
    {
        foreach (OrderBoardController board in _boards)
        {
            board.OrderCompleted += Board_OrderCompleted;
        }

        CheckDifficulty();
        ResetBoards();
        GlobalVariables.Instance.CurrentTime = _currentDifficultyModifier.MaxTimePerOrder;

        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (GlobalVariables.Instance.TimerStarted)
        {
            GlobalVariables.Instance.CurrentTime -= Time.deltaTime;
        }


        if(GlobalVariables.Instance.CurrentTime < 0 && _gameOver == false)
        {
            _cameraController.GoToMiddle(() => { SceneManager.LoadScene(_mainMenuScene); });
            _gameOverEvent?.Invoke();
            _gameOver = true;
            GlobalVariables.Instance.GameOver = true;

            MusicManager.Instance.StopAllMusic();

            if(GlobalVariables.Instance.EndlessMode && GlobalVariables.Instance.HighScore < GlobalVariables.Instance.CompletedOrders)
            {
                GlobalVariables.Instance.HighScore = GlobalVariables.Instance.CompletedOrders;
            }
        }

        if(!GlobalVariables.Instance.EndlessMode && GlobalVariables.Instance.CompletedOrders >= _completeGameThreshold && GlobalVariables.Instance.CurrentTime > 0 && _gameOver == false)
        {
            _gameOver = true;
            _cameraController.GoToMiddle(() => { SceneManager.LoadScene(_mainMenuScene); });
            GlobalVariables.Instance.GameComplete = true;
            _gameCompleteEvent?.Invoke();
            MusicManager.Instance.StopAllMusic();
        }
    }

    private void Board_OrderCompleted(object sender, EventArgs e)
    {
        if (GlobalVariables.Instance.CurrentTime < 0)
        {
            return;
        }
        GlobalVariables.Instance.CompletedOrders++;
        CheckDifficulty();
        ResetBoards();
        GlobalVariables.Instance.CurrentTime = _currentDifficultyModifier.MaxTimePerOrder + 1;
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

            if(_currentDifficultyModifier.ApearText != null)
            {
                _currentDifficultyModifier.ApearText.Appear();
            }


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
