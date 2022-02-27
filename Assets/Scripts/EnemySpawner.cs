using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject tankerPrefab;
    [SerializeField] GameObject hitterPrefab;
    [SerializeField] GameObject casterPrefab;
    [SerializeField] float timeBetweenSpawnWaves = 5;

    float spawnDelay;

    [SerializeField] Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = timeBetweenSpawnWaves;

        int i = 0;
        foreach (var location in spawnPoints) {
            switch (i) {
                    case 0:
                        Instantiate(hitterPrefab, location);
                        break;
                    case 1:
                        Instantiate(tankerPrefab, location);
                        break;
                    case 2:
                        Instantiate(casterPrefab, location);
                        break;
                    default:
                        Debug.Log("Paso algo raro, esto no deberia salir");
                        break;
                }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int enemyNum = Random.Range(0, 3);


        if(spawnDelay <= 0) {
            foreach (var location in spawnPoints)
            {
                switch (enemyNum) {
                    case 0:
                        Instantiate(hitterPrefab, location);
                        break;
                    case 1:
                        Instantiate(tankerPrefab, location);
                        break;
                    case 2:
                        Instantiate(casterPrefab, location);
                        break;
                    default:
                        Debug.Log("Paso algo raro, esto no deberia salir");
                        break;
                     
                }
            }
       
            spawnDelay = timeBetweenSpawnWaves;
        } else {
            spawnDelay -= Time.deltaTime;
        }

        
    }
}
