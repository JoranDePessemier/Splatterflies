using System;
using UnityEngine;

public class WasCaughtEventArgs : EventArgs
{
    public WasCaughtEventArgs(ButterflyType type)
    {
        this.Type = type;
    }

    public ButterflyType Type { get; private set; }
}

public class BaseButterflyFlying : MonoBehaviour
{
    protected Rigidbody2D _body;

    public ButterflyType Type { get;protected set; }

    private bool _isCaught;

    public event EventHandler<WasCaughtEventArgs> WasCaught;
    public event EventHandler<WasCaughtEventArgs> LeftScene;


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
        OnWasCaught(new WasCaughtEventArgs(Type));
    }

    public void Left()
    {
        OnLeft(new WasCaughtEventArgs(Type));
        Destroy(this.gameObject);
    }

    private void OnWasCaught(WasCaughtEventArgs eventArgs)
    {
        var handler = WasCaught;
        handler?.Invoke(this, eventArgs);
    }

    private void OnLeft(WasCaughtEventArgs eventArgs)
    {
        var handler = LeftScene;
        handler?.Invoke(this, eventArgs);
    }


}
