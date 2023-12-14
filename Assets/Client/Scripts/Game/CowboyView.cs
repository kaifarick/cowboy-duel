using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RevolverView _revolverView;

    private void Shot()
    {
        Debug.Log("Shot");
        _animator.SetTrigger("Shot");
    }

    private void TakeGun()
    {
        _revolverView.Show();
    }

    private void PutGun()
    {
        _revolverView.Hide();
    }
    
    
    private void OnMouseDown()
    {
        Shot();
    }
}
