using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRammer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _explosion;
    public float _distance;
    Transform Player;
    Rigidbody2D move;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector3.Distance(transform.position, Player.transform.position);
        move.AddForce((Player.transform.position - transform.position) * 0.04f);
        Quaternion rotation = Quaternion.LookRotation(Player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        if (transform.position.x >= 12)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

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
}
