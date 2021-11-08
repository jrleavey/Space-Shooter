using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRammer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosion;
    public float _distancetoPlayer;
    Transform PlayerLocation;
    Rigidbody2D move;
    void Start()
    {
        move = GetComponent<Rigidbody2D>();
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ChasePlayer();
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

            Instantiate(_explosion, transform.position, Quaternion.identity);

            _speed = 0;

            _audioSource.Play();

            SpawnManager.Instance.OnEnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(GetComponent<SpriteRenderer>(), 0.25f);

            Destroy(this.gameObject, 2f);
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            Instantiate(_explosion, transform.position, Quaternion.identity);

            Destroy(GetComponent<SpriteRenderer>(), 0.25f);

            _speed = 0;

            _audioSource.Play();

            SpawnManager.Instance.OnEnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2f);
        }
        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
                Debug.Log("Hit by Missile");
            }

            Instantiate(_explosion, transform.position, Quaternion.identity);

            _speed = 0;

            Destroy(GetComponent<SpriteRenderer>(), 0.25f);

            _audioSource.Play();

            SpawnManager.Instance.OnEnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2f);
        }
    }
    void ChasePlayer()
    {
        float step = (_speed) * Time.deltaTime * 3;
        transform.position = Vector2.MoveTowards(transform.position, PlayerLocation.position, step);
        transform.up = (PlayerLocation.position - transform.position) * -1;
        transform.Rotate(0, 0, -90);
    }
}
