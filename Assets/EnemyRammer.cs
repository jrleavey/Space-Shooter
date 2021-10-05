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

        if ( _distance < 5)
        {
            move.AddForce((Player.transform.position - transform.position) * 1);
        }
        if (transform.position.x > 12f)
        {
            float randomY = Random.Range(-4f, 6f);
            transform.position = new Vector3(-12, randomY, 0);
        }
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

    }
    void CalculateMovementSideToSide()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x > 12f)
        {
            float randomY = Random.Range(-4f, 6f);
            transform.position = new Vector3(-12, randomY, 0);
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

            Instantiate(_explosion, transform.position, Quaternion.identity);

            _speed = 0;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(GetComponent<SpriteRenderer>(), 0.25f);
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

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2f);
        }
        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            Instantiate(_explosion, transform.position, Quaternion.identity);

            _speed = 0;

            Destroy(GetComponent<SpriteRenderer>(), 0.25f);

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2f);
        }
    }
}
