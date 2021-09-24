using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f; 
    [SerializeField] //0 = Triple shot 1 = Speed 2 = Shields
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
      
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

                    default:
                        Debug.Log("Default Value");
                            break;
                }
            }

            Destroy(this.gameObject);
        }

            
    }
}
