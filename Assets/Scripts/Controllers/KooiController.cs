using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KooiController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private LayerMask _butterflyMask;

    [SerializeField]
    private ButterflyType _typeToHold;

    [SerializeField]
    private int _activationClicks;

    [SerializeField]
    private Transform _launchTransform;

    [SerializeField]
    private float _launchForce;

    private BaseButterflyDungeon _holdingButterfly;

    private int _currentClicks;

    [SerializeField]
    private UnityEvent _close;

    [SerializeField]
    private UnityEvent _open;

    [SerializeField]
    private UnityEvent _clicked;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_holdingButterfly == null)
        {
            GameObject collisionObject = collision.gameObject;

            collisionObject.TryGetComponent<BaseButterflyDungeon>(out BaseButterflyDungeon butterfly);

            if (Utilities.IsInMask(collisionObject, _butterflyMask) && butterfly.Type == _typeToHold && butterfly.PlayerIsHolding)
            {
                _holdingButterfly = butterfly;
                butterfly.SetInactive();
                _currentClicks = 0;
                _close.Invoke();
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_holdingButterfly == null)
        {
            return;
        }

        _currentClicks++;
        _clicked.Invoke();

        if(_currentClicks >= _activationClicks)
        {
            Complete();
        }
    }

    private void Complete()
    {
        Rigidbody2D deadBody = _holdingButterfly.Completed().GetComponent<Rigidbody2D>();
        deadBody.position = _launchTransform.position;
        deadBody.AddForce(_launchTransform.up * _launchForce, ForceMode2D.Impulse);
        _holdingButterfly = null;
        _open.Invoke();
    }
}
