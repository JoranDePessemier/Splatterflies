using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _startXYSpawnArea;

    [SerializeField]
    private Vector2 _endXYSpawnArea;

    [SerializeField]
    private List<BaseButterflyFlying> _spawnObjects;

    [SerializeField]
    private float _amountPerButterfly = 3;


    private List<ButterflyType> _spawnTypes = new List<ButterflyType>();

    private void Start()
    {
        foreach(BaseButterflyFlying fly in _spawnObjects)
        {
            _spawnTypes.Add(fly.Type);
            for (int i = 0; i < _amountPerButterfly; i++)
            {
                SpawnButterfly(fly.Type);
            }
        }
    }

    private void SpawnButterfly(ButterflyType type)
    {
        Transform flyTransform = _spawnObjects[_spawnTypes.IndexOf(type)].transform;

        Vector2 spawnPoint = DetermineSpawnPoint();

        BaseButterflyFlying spawnedFly = GameObject.Instantiate(flyTransform, spawnPoint, flyTransform.rotation).GetComponent<BaseButterflyFlying>();
        spawnedFly.WasCaught += (s, e) => SpawnButterfly(e.Type);
        spawnedFly.LeftScene += (s, e) => SpawnButterfly(e.Type);
    }

    private Vector2 DetermineSpawnPoint()
    {
        float x = UnityEngine.Random.Range(0,2) == 1 ?  _startXYSpawnArea.x : _endXYSpawnArea.x;
        float y = UnityEngine.Random.Range(_startXYSpawnArea.y,_endXYSpawnArea.y);

        return new Vector2(x,y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(_endXYSpawnArea.x, _startXYSpawnArea.y, 0), new Vector3(_endXYSpawnArea.x, _endXYSpawnArea.y, 0));
        Gizmos.DrawLine(new Vector3(_startXYSpawnArea.x, _startXYSpawnArea.y, 0), new Vector3(_startXYSpawnArea.x, _endXYSpawnArea.y, 0));
    }

}
