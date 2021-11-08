using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterN : MonoBehaviour
{
    [SerializeField]
    private Slider _thrusterBarSlider;
    [SerializeField]
    private float _maxThruster, _thruster;
    void Start()
    {
        _thrusterBarSlider = GetComponent<Slider>();
        _thruster = _maxThruster;
        _thrusterBarSlider.maxValue = _maxThruster;
    }
    void Update()
    {
        ToggleThrust();
    }
    public float GetThruster()
    {
        return _thruster;
    }

    void ToggleThrust()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _thruster -= Time.deltaTime;
            _thrusterBarSlider.value = _thruster;
            if (_thruster <= 0)
            {
                _thruster = 0;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            _thruster += Time.deltaTime;
            _thrusterBarSlider.value = _thruster;
            if (_thruster > 5)
            {
                _thruster = _maxThruster;
            }
        }
    }
}
