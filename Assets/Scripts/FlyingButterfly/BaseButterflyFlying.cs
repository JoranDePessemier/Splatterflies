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

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    protected void UpdateMovement()
    {
        
    }

    public void Caught(Vector2 movementPosition, float movementSpeed)
    {
        StartCoroutine(Utilities.MoveToPoint(movementPosition,() => Destroy(this.gameObject),_body,movementSpeed)); 
    }
}
