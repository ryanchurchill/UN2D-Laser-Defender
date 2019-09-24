using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // config
    [SerializeField] List<WaveConfig> waveConfigs;

    // other
    int startingWaveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWaveIndex];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        

    }
}
