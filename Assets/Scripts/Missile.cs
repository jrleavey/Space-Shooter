using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7.0f;
    [SerializeField]
    public Transform _target;

    void Start()
    {
        TrackTarget();
    }
    void Update()
    {
        if(_target != null)
        {
            
            ChaseTarget();

        }
        else if (_target == null)
        {
            TrackTarget();
            ChaseTarget();
            
        }
    }
    void TrackTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestTarget = null;

        float distance, mindistance = Mathf.Infinity;

        for (int x = 0; x < targets.Length; x++)
        {
            distance = Vector3.Distance(transform.position, targets[x].transform.position);

            if (distance < mindistance)
            {
                mindistance = distance;
                closestTarget = targets[x];
            }
        }

        _target = closestTarget.transform;
    }
    void ChaseTarget()
    {
        Vector3 direction = _target.position - transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(Vector3.forward * (angle - 90f));

        transform.Translate(transform.up * _speed * Time.deltaTime);
    }
}
