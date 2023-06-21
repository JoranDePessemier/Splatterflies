using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonButterflySpawner : MonoBehaviour
{

    [SerializeField]
    private List<BaseButterflyDungeon> _spawnObjects;

    private List<ButterflyType> _spawnTypes = new List<ButterflyType>();

    [SerializeField]
    private Vector2 _butterflySpawnPoint;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(_butterflySpawnPoint, "character_Mode");
    }

    private void Start()
    {
        foreach (BaseButterflyDungeon fly in _spawnObjects)
        {
            _spawnTypes.Add(fly.Type);
        }
    }

    internal void SpawnButterfly(object sender, WasCaughtEventArgs e)
    {
        Transform flyTransform = _spawnObjects[_spawnTypes.IndexOf(e.Type)].transform;

        BaseButterflyDungeon spawnedFly = GameObject.Instantiate(flyTransform, _butterflySpawnPoint, flyTransform.rotation).GetComponent<BaseButterflyDungeon>();
    }
}
