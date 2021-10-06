using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyRammer;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private bool _stopSpawning = false;
    private int _WaveID;
    private int _enemyDeathcount;
    private int[] _WaveCount = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
    private int _enemyCount;

    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Spawn Manager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        //StartCoroutine(SpawnRammerRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            while (_enemyDeathcount < _WaveCount[_WaveID])
            {
                while(_enemyCount <= _WaveCount[_WaveID])
                {
                    int rng = Random.Range(0, 100);
                    if (rng >= 30)
                    {
                        Vector3 postospawn = new Vector3(Random.Range(-9f, 9f), 9f, 0);
                        GameObject newEnemy = Instantiate(_enemyPrefab, postospawn, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                        _enemyCount++;
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        Vector3 rampostospawn = new Vector3(-12f, Random.Range(-4f, 6f), 0);
                        GameObject newRamEnemy = Instantiate(_enemyRammer, rampostospawn, Quaternion.identity);
                        newRamEnemy.transform.parent = _enemyContainer.transform;
                        _enemyCount++;
                        yield return new WaitForSeconds(2f);
                    }
                }
                yield return null;
            }
            _enemyCount = 0;
            _enemyDeathcount = 0;
            _WaveID++;
            // update wave txt
            yield return new WaitForSeconds(10.0f);
        }
        
    }
    IEnumerator SpawnRammerRoutine()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("StartRamRoutine");
        while (_stopSpawning == false)
        {
            Debug.Log("_StopSpawning");

            while (_enemyDeathcount < _WaveCount[_WaveID])
            {
                Debug.Log("_EnemyDeathcount");

                while (_enemyCount <= _WaveCount[_WaveID])
                {
                    Debug.Log("_enemyCount");
                    Vector3 rampostospawn = new Vector3(-12f, Random.Range(-4f, 6f), 0);
                    GameObject newRamEnemy = Instantiate(_enemyRammer, rampostospawn, Quaternion.identity);
                    newRamEnemy.transform.parent = _enemyContainer.transform;
                    _enemyCount++;
                    yield return new WaitForSeconds(2f);

                }
                yield return null;
            }
            _enemyCount = 0;
            _enemyDeathcount = 0;
            _WaveID++;
            yield return new WaitForSeconds(10.0f);
        }

    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 posttospawn = new Vector3(Random.Range(-9f, 9f), 9f, 0);
            int randomPowerUp = Random.Range(0, 7);
            Instantiate(_powerups[randomPowerUp], posttospawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    public void OnEnemyDeath()
    {
        _enemyDeathcount++;
    }
}