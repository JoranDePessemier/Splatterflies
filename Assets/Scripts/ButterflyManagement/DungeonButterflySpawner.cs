using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonButterflySpawner : MonoBehaviour
{

    [SerializeField]
    private List<BaseButterflyDungeon> _spawnObjects;

    private List<ButterflyType> _spawnTypes = new List<ButterflyType>();

    private FlyingButterflySpawner _flyingSpawner;

    [SerializeField]
    private Vector2 _butterflySpawnPoint;

    private CameraController _cameraController;

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

        _flyingSpawner = this.GetComponent<FlyingButterflySpawner>();
        _cameraController = FindObjectOfType<CameraController>();
    }

    internal void SpawnButterfly(object sender, WasCaughtEventArgs e)
    {
        Transform flyTransform = _spawnObjects[_spawnTypes.IndexOf(e.Type)].transform;

        BaseButterflyDungeon spawnedFly = GameObject.Instantiate(flyTransform, _butterflySpawnPoint, flyTransform.rotation).GetComponent<BaseButterflyDungeon>();
        spawnedFly.WasCompleted += (s, e) => { _flyingSpawner.SpawnButterfly(e.Type); _cameraController.ScreenShake(); };
    }
}
