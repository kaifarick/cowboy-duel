using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public static Coroutine StartCoroutineStatic(IEnumerator routine){

        return _instance.StartCoroutine(routine);
    }

    public static void StopCoroutineStatic(IEnumerator routine){
        _instance.StopCoroutine(routine);
    }
    public static void StopCoroutineStatic(Coroutine routine){
        _instance.StopCoroutine(routine);
    }
}
