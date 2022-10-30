using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public enum SpawnState { SPAWNING, WAITNG, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int health;
        public int count;
        public float rate;

        public int GetHealth()
        {
            return health;
        }
    }



    public Wave[] waves;
    public int waveIndex = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    private int level;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1");
            return;
        }

        instance = this;
    }

    void Start() 
    {
        waveCountdown = timeBetweenWaves;
        level = SceneManager.GetActiveScene().name == "Level 1" ? 1 : 2;

    }

    void Update()
    {
        if(state == SpawnState.WAITNG)
        {
            if (waveIndex < 9 && !EnemyIsAlive())
                waveIndex++;
            else
                return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start wave if countdown is 0
                StartCoroutine(SpawnWave(waves[waveIndex]));
                
            }
        }
        else 
        {
            waveCountdown -= Time.deltaTime;
        }
        
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null || waveIndex >= 6)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        //Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.rate);
        }

        state = SpawnState.WAITNG;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy");

        Transform spawn1 = GameObject.FindGameObjectWithTag("Spawn1").transform;
        
        if (level == 1)
        {
            Transform spawn2 = GameObject.FindGameObjectWithTag("Spawn2").transform;

            int rand = Random.Range(0, 2);
            if (rand == 1)
                Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);
            else
                Instantiate(_enemy, spawn2.transform.position, spawn2.transform.rotation);
        }
        else
            Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);
    }


}
