using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private BaseButterflyDungeon _holdingButterfly;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_holdingButterfly == null)
        {
            GameObject collisionObject = collision.gameObject;

            collisionObject.TryGetComponent<BaseButterflyDungeon>(out _holdingButterfly);

            if (Utilities.IsInMask(collisionObject, _butterflyMask) && _holdingButterfly.Type == _typeToHold)
            {
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
    }
}
