using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHolderDownScreen : MonoBehaviour
{
    private GameObject _child;

    private MainMenuManager _menuManager;

    [SerializeField]
    private MenuState _visibleState;

    private bool _isVisible;

    private void Awake()
    {
        _menuManager = FindObjectOfType<MainMenuManager>();
        _child = GetComponentInChildren<Canvas>().gameObject;  
    }

    private void Update()
    {
        if (_menuManager.State == _visibleState || (_isVisible && (_menuManager.State == MenuState.Main || _menuManager.State == MenuState.StartGame)))
        {
            _isVisible = true;
            _child.SetActive(true);
        }
        else
        {
            _isVisible = false;
            _child.SetActive(false);
        }
    }
}
