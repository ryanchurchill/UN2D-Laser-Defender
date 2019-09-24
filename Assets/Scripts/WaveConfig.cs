using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2;

    public GameObject GetEnemyPrefab { get => enemyPrefab; }
    public float GetTimeBetweenSpawns { get => timeBetweenSpawns; }
    public float GetSpawnRandomFactor { get => spawnRandomFactor; }
    public int GetNumberOfEnemies { get => numberOfEnemies; }
    public float GetMoveSpeed { get => moveSpeed; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        
        // wtf?
        foreach (Transform t in pathPrefab.transform)
        {
            waveWaypoints.Add(t);
        }

        return waveWaypoints;
    }

}
