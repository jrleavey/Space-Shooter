using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private int _EnemyDirection;
    private float _fireRate = 3f;
    private float _canFire = -1;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private bool _isShieldOn;
    [SerializeField]
    private GameObject _shield;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
        int X = Random.Range(0, 100);
        if (X >= 50)
        {
            _isShieldOn = true;
        }
        else
        {
            _isShieldOn = false;
        }
        _shield.SetActive(_isShieldOn);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementToptoBottom();

        if (Time.deltaTime > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }
    }

    void CalculateMovementToptoBottom()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 9f, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)

            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0;

            _audioSource.Play();

            SpawnManager.Instance.OnEnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2f);
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_isShieldOn == true && _shield.activeInHierarchy == true)
            {
                _shield.SetActive(false);
                _isShieldOn = false;
                return;
            }
            else
            {
                if (_player != null)
                {
                    _player.AddScore(10);
                }
                _anim.SetTrigger("OnEnemyDeath");

                _speed = 0;

                _audioSource.Play();

                SpawnManager.Instance.OnEnemyDeath();

                Destroy(GetComponent<Collider2D>());

                Destroy(this.gameObject, 2f);
            }
        }
        if (other.tag == "Missile")
        {
            {
                Destroy(other.gameObject);
                if (_isShieldOn == true && _shield.activeInHierarchy == true)
                {
                    _shield.SetActive(false);
                    _isShieldOn = false;
                    return;
                }
                else
                {
                    if (_player != null)
                    {
                        _player.AddScore(10);
                    }
                    _anim.SetTrigger("OnEnemyDeath");

                    _speed = 0;

                    _audioSource.Play();

                    SpawnManager.Instance.OnEnemyDeath();

                    Destroy(GetComponent<Collider2D>());

                    Destroy(this.gameObject, 2f);
                }
            }
        }
    }
}
