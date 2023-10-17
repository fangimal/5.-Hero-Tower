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
    private CharacterController _characterController;
    private ThirdPersonController _thirdPersonController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        if (!_thirdPersonController.Grounded)
        {
            if (Physics.Raycast(transform.position, Vector3.down, distance, layerMask))
            {
                Debug.Log("Distanse less then: " + distance);
            }
            else
            {
                _characterController.enabled = false;
                OnEnabled?.Invoke();
                Debug.Log("More then: " + distance);
            }
        }
    }

    public void OnCharacterController()
    {
        _characterController.enabled = true;
    }
}
