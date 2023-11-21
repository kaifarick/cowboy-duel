using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Shot()
    {
        Debug.Log("Shot");
        _animator.SetTrigger("Shot");
    }

    private void OnMouseDown()
    {
        Shot();
    }
}
