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
    private int _ammo = 15;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldfull;
    [SerializeField]
    private GameObject _shieldpartial;
    [SerializeField]
    private GameObject _shieldminimal;
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
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _noAmmoSoundClip;
    [SerializeField]
    private AudioSource _audioSource2;
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateLives(_lives);
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
            _speed = _speed + 3f;
            _Boostedspeed = _Boostedspeed + 3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = _speed - 3f;
            _Boostedspeed = _Boostedspeed - 3f;
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
            _audioSource.Play();
        }
        else if (_ammo > 0)
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _ammo = _ammo - 1;
            _audioSource.Play();
            _uiManager.UpdateAmmo(_ammo);
        }
        else if(_ammo == 0)
        {
            _audioSource2.Play();
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true && _shieldfull.activeInHierarchy == true)
        {
            _shieldfull.SetActive(false);
            _shieldpartial.SetActive(true);
            return;
        }
        else if (_isShieldActive == true && _shieldpartial.activeInHierarchy == true)
        {
            _shieldpartial.SetActive(false);
            _shieldminimal.SetActive(true);
            return;
        }
        else if (_isShieldActive == true && _shieldminimal.activeInHierarchy == true)
        {
            _shieldminimal.SetActive(false);
            _isShieldActive = false;
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
        _shieldfull.SetActive(true);
        _shieldpartial.SetActive(false);
        _shieldminimal.SetActive(false);
        _isShieldActive = true;

        StartCoroutine(ShieldPowerDownRoutine());

        IEnumerator ShieldPowerDownRoutine()
        {
            yield return new WaitForSeconds(15.0f);
            _isShieldActive = false;
            _shieldfull.SetActive(false);
            _shieldpartial.SetActive(false);
            _shieldminimal.SetActive(false);
        }
    }

    public void GetAmmo()
    {
        _ammo += 15;
        _uiManager.UpdateAmmo(_ammo);

    }

    public void AddLife()
    {
        if (_lives < 3)
        {
            _lives = _lives + 1;
            _uiManager.UpdateLives(_lives);
        }         
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
    