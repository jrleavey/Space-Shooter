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

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnRammerRoutine());
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
            Vector3 postospawn = new Vector3(Random.Range(-9f, 9f), 9f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postospawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.0f);
        }
        
    }
    IEnumerator SpawnRammerRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 rampostospawn = new Vector3(12f, Random.Range(-4f,6f), 0);
            GameObject newRamEnemy = Instantiate(_enemyRammer, rampostospawn, Quaternion.identity);
            newRamEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
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
}

