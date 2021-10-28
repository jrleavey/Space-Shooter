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
    private int[] rarity = { 
        70,
        5,
        5,
        5,
        5,
        5,
        5
    };
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private int _WaveID;
    private int _enemyDeathcount;
    private int[] _WaveCount = {1,2,3,4,5,6,7,8,9,10};
    private int _enemyCount;
    [SerializeField]
    private Player _player;


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
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false & _WaveID <= _WaveCount.Length)
        {
            while (_enemyDeathcount < _WaveCount[_WaveID])
            {
                while(_enemyCount < _WaveCount[_WaveID])
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
            _player.AddWave(1);
            yield return new WaitForSeconds(2f);
        }
        if (_WaveID > _WaveCount.Length)
        {
            //spawn boss
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 posttospawn = new Vector3(Random.Range(-9f, 9f), 9f, 0);
            int randomPowerUp = randomTable();
            Instantiate(_powerups[randomPowerUp], posttospawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    int randomTable()
    {
        int rng = Random.Range(1, 101);
        for (int i = 0; i < rarity.Length; i++)
        {
            if (rng <= rarity[i])
            {
                Debug.Log("RnG is " + i);
                return i;
            }
            else
            {
                rng -= rarity[i];
            }
        }
        return 0;
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