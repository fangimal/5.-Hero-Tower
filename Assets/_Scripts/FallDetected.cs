using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class FallDetected : MonoBehaviour
{
    public float distance = 10f;
    public LayerMask layerMask;

    public event Action OnEnabled;
    private ThirdPersonController _thirdPersonController;

    private float _startTimer = 2f;
    private float _timer;
    

    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _timer = _startTimer;
    }

    private void FixedUpdate()
    {
        if (_thirdPersonController.Grounded)
        {
            _timer = _startTimer;
        }
        
        if (!_thirdPersonController.Grounded && _timer < 0)
        {
            if (Physics.Raycast(transform.position, Vector3.down, distance, layerMask))
            {
                //Debug.Log("Distanse less then: " + distance);
            }
            else
            {
                //_characterController.enabled = false;
                OnEnabled?.Invoke();
                //Debug.Log("More then: " + distance);
            }
        }

        StartFallTimer();
    }

    private void StartFallTimer()
    {
        if (!_thirdPersonController.Grounded)
        {
            _timer -= Time.deltaTime;
        }
    }
    
}
