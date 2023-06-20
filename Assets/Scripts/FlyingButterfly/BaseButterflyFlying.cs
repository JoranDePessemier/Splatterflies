using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BaseButterflyFlying : MonoBehaviour
{
    protected Rigidbody2D _body;

    public ButterflyType Type { get;protected set; }

    private bool _isCaught;


    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!_isCaught)
        {
            UpdateMovement();
        }

    }

    protected virtual void UpdateMovement()
    {
        
    }

    public void Caught(Vector2 movementPosition, float movementSpeed)
    {
        StartCoroutine(Utilities.MoveToPoint(movementPosition, () => Destroy(this.gameObject), _body, movementSpeed));
        _isCaught = true;
    }
}
