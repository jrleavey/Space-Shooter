using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f; 
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;
    public float _distance;
    Transform PlayerLocation;
    Rigidbody2D move;
    void Start()
    {
        move = GetComponent<Rigidbody2D>();
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        TractorBeamLogic(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.GetAmmo();
                        break;
                    case 4:
                        player.AddLife();
                        break;
                    case 5:
                        player.MissilesLauncher();
                        break;
                    case 6:
                        player.FreezeActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                            break;
                }
            }

            Destroy(this.gameObject);
        }

            
    }
    void TractorBeamLogic()
    {
        if (PlayerLocation != null)
        {
            _distance = Vector3.Distance(transform.position, PlayerLocation.transform.position);
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C) && _distance < 5)
        {
            move.AddForce((PlayerLocation.transform.position - transform.position) * 1);
        }

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
