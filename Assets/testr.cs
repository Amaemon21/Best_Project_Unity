using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testr : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.SetTrigger("T");
        }
    }
}
