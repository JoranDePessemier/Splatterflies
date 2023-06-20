using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyDestroyer : MonoBehaviour
{
    [SerializeField]
    private LayerMask _butterflyLayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (Utilities.IsInMask(collisionObject, _butterflyLayerMask))
        {
            if (!collisionObject.TryGetComponent<BaseButterflyFlying>(out BaseButterflyFlying butterfly))
            {
                Debug.LogError("A butterfly that is in the flying layer does not have the butterfly flying script, you stupid");
            }

            butterfly.Left();

        }
    }

   
}

