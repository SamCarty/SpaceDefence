using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool looping = false;
    [SerializeField] private int startingWave = 0;

    private IEnumerator Start()
    {
        do 
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (var i = startingWave; i < waveConfigs.Count; i++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[i]));
        }
    }
    
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (var i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            var enemy = Instantiate(
                wave.GetEnemyPrefab(),
                wave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns() + Random.Range(0, wave.GetSpawnRandomFactor()));
        }
    }
}
