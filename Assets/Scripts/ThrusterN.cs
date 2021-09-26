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
    // Start is called before the first frame update
    void Start()
    {
        _thrusterBarSlider = GetComponent<Slider>();
        _thruster = _maxThruster;
        _thrusterBarSlider.maxValue = _maxThruster;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
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
    public float GetThruster()
    {
        return _thruster;
    }
}
