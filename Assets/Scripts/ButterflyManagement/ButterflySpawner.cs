using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _startXYSpawnArea;

    [SerializeField]
    private Vector2 _endXYSpawnArea;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_endXYSpawnArea.x, _startXYSpawnArea.y, 0), new Vector3(_endXYSpawnArea.x, _endXYSpawnArea.y, 0));
        Gizmos.DrawLine(new Vector3(_startXYSpawnArea.x, _startXYSpawnArea.y, 0), new Vector3(_startXYSpawnArea.x, _endXYSpawnArea.y, 0));
    }
}
