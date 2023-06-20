using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static Vector2 GetMousePositionWorldSpace(Controls controls, Camera cam)
    {
        Vector2 mouseposScreen = controls.PlayerInput.MousePosition.ReadValue<Vector2>();
        return cam.ScreenToWorldPoint(new Vector3(mouseposScreen.x, mouseposScreen.y, 10));
    }
}
