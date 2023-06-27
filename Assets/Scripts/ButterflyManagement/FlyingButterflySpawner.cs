using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingButterflySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 _startXYSpawnArea;

    [SerializeField]
    private Vector2 _endXYSpawnArea;

    [SerializeField]
    private List<BaseButterflyFlying> _spawnObjects;

    [SerializeField]
    private float _amountPerButterfly = 3;

    private List<int> _amountPerSpawnObject = new List<int>();

    private int _currentButterflySpawnAmount; 

    DungeonButterflySpawner _dungeonSpawner;
    MainOrderController _mainOrderController;


    private List<ButterflyType> _spawnTypes = new List<ButterflyType>();
    private void Awake()
    {
        foreach(BaseButterflyFlying b in _spawnObjects)
        {
            _amountPerSpawnObject.Add(0);
        }
        _dungeonSpawner = this.GetComponent<DungeonButterflySpawner>();
        _mainOrderController = this.GetComponent<MainOrderController>();
        _mainOrderController.ModifyDifficulty += ModifySpawningButterflies;

    }

    private void ModifySpawningButterflies(object sender, ModifyDifficultyEventArgs e)
    {
        for (int i = _currentButterflySpawnAmount; i < e.CurrentModifier.ButterflyTypeAmount; i++)
        {
            BaseButterflyFlying fly = _spawnObjects[i];

            _spawnTypes.Add(fly.Type);
            for (int a = 0; a < _amountPerButterfly; a++)
            {
                SpawnButterfly(fly.Type);
            }
        }

        _currentButterflySpawnAmount = e.CurrentModifier.ButterflyTypeAmount;
    }

    public void SpawnButterfly(ButterflyType type)
    {
        int spawnIndex = _spawnTypes.IndexOf(type);

        if (_amountPerSpawnObject[spawnIndex] >= _amountPerButterfly)
        {
            return;
        }

        Transform flyTransform = _spawnObjects[spawnIndex].transform;

        _amountPerSpawnObject[spawnIndex]++;

        Vector2 spawnPoint = DetermineSpawnPoint();

        BaseButterflyFlying spawnedFly = GameObject.Instantiate(flyTransform, spawnPoint, flyTransform.rotation).GetComponent<BaseButterflyFlying>();
        spawnedFly.LeftScene += (s, e) => { _amountPerSpawnObject[spawnIndex]--; SpawnButterfly(e.Type); };
        spawnedFly.WasCaught += _dungeonSpawner.SpawnButterfly;

        spawnedFly.WasCaught += (s,e) => _amountPerSpawnObject[spawnIndex]--;
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
