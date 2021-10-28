using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotatespeed = 3.0f;
    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotatespeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _spawnManager.StartSpawning();

            Destroy(gameObject, 0.2f);

        }
        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _spawnManager.StartSpawning();

            Destroy(gameObject, 0.2f);

        }
    }
}
