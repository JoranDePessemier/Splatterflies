using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KachelController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _butterflyMask;

    [SerializeField]
    private ButterflyType _typeToHold;

    [SerializeField]
    private float _activationTime;

    [SerializeField]
    private Transform _launchTransform;

    [SerializeField]
    private float _launchForce;

    [SerializeField]
    private UnityEvent _close;

    [SerializeField]
    private UnityEvent _open;

    private BaseButterflyDungeon _holdingButterfly;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_holdingButterfly == null)
        {
            GameObject collisionObject = collision.gameObject;

            collisionObject.TryGetComponent<BaseButterflyDungeon>(out BaseButterflyDungeon butterfly);

            if (Utilities.IsInMask(collisionObject, _butterflyMask) && butterfly.Type == _typeToHold && butterfly.PlayerIsHolding)
            {
                _holdingButterfly = butterfly;
                _close.Invoke();
                StartCoroutine(WaitForActivationTime());
            }
        }

    }

    private IEnumerator WaitForActivationTime()
    {
        _holdingButterfly.SetInactive();
        yield return new WaitForSeconds(_activationTime);
        Rigidbody2D deadBody = _holdingButterfly.Completed().GetComponent<Rigidbody2D>();
        deadBody.position = _launchTransform.position;


        deadBody.AddForce(_launchTransform.up * _launchForce, ForceMode2D.Impulse);
        _holdingButterfly = null;
        _open.Invoke();

    }
}
