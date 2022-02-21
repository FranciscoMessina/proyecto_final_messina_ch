using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject tankerPrefab;
    [SerializeField] GameObject hitterPrefab;
    [SerializeField] GameObject casterPrefab;

    [SerializeField] int tankerAmount = 1;
    [SerializeField] int hitterAmount = 1;
    [SerializeField] int casterAmount = 1;

    [SerializeField] float timeBetweenSpawnWaves = 5;

    float spawnDelay;

    [SerializeField] Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = timeBetweenSpawnWaves;
        int randomNumber = Random.Range(0, spawnPoints.Length);
        int randomNumber2 = Random.Range(0, spawnPoints.Length);
        int randomNumber3 = Random.Range(0, spawnPoints.Length);
        if (tankerAmount > 0) {
            Instantiate(tankerPrefab, spawnPoints[randomNumber]);
            tankerAmount--;
        }
        if (hitterAmount > 0) {
            Instantiate(hitterPrefab, spawnPoints[randomNumber2]);
            hitterAmount--;
        }
        if (casterAmount > 0) {
            Instantiate(casterPrefab, spawnPoints[randomNumber3]);
            casterAmount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        int randomNumber2 = Random.Range(0, spawnPoints.Length);
        int randomNumber3 = Random.Range(0, spawnPoints.Length);

        if(spawnDelay <= 0) {
            if (tankerAmount > 0) {
                Instantiate(tankerPrefab, spawnPoints[randomNumber]);
                tankerAmount--;
            }
            if (hitterAmount > 0) {
                Instantiate(hitterPrefab, spawnPoints[randomNumber2]);
                hitterAmount--;
            }
            if (casterAmount > 0) {
                Instantiate(casterPrefab, spawnPoints[randomNumber3]);
                casterAmount--;
            }
            spawnDelay = timeBetweenSpawnWaves;
        } else {
            spawnDelay -= Time.deltaTime;
        }

        
    }
}
