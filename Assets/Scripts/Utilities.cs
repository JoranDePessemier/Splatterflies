using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public static class Utilities 
{
    public static Vector2 GetMousePositionWorldSpace(Controls controls, Camera cam)
    {
        Vector2 mouseposScreen = controls.PlayerInput.MousePosition.ReadValue<Vector2>();
        return cam.ScreenToWorldPoint(new Vector3(mouseposScreen.x, mouseposScreen.y, 10));
    }

    public static bool IsInMask(GameObject maskObject, LayerMask mask)
    {
        return ((mask & (1 << maskObject.layer)) != 0);
    }

    public static IEnumerator MoveToPoint(Vector2 point, Action OnMoveCompleted, Rigidbody2D body, float movementSpeed)
    {
        while (body.position != point)
        {
            Vector2 nextPosition = Vector2.MoveTowards(body.position, point, movementSpeed * Time.deltaTime);
            body.MovePosition(nextPosition);
            yield return new WaitForFixedUpdate();
        }

        OnMoveCompleted();
    }
}
