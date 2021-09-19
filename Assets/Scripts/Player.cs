using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _Boostedspeed = 8f;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }


        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

    }


    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed = _speed * 2;
            _Boostedspeed = _Boostedspeed * 2f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = _speed * 0.5f;
            _Boostedspeed = _Boostedspeed * 0.5f;
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedActive == true)
        {
            transform.Translate(direction * _Boostedspeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)

            transform.position = new Vector3(transform.position.x, 0, 0);

        else if (transform.position.y <= -3.8f)

            transform.position = new Vector3(transform.position.x, -3.8f);

        if (transform.position.x > 11.4f)

            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        else if (transform.position.x < -11.3f)

            transform.position = new Vector3(11.4f, transform.position.y, 0);
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        _audioSource.Play();



    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
            _lives--;
       
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerdownRoutine());

        IEnumerator TripleShotPowerdownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerDownRoutine());

        IEnumerator SpeedPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedActive = false;
        }



    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());

        IEnumerator ShieldPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
        }
    }


    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
    