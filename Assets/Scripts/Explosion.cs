using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionSound;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }
}