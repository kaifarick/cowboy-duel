using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimerManager
{
    private class WaitCall
    {
        public Action Callback;
        public Action EverySecondAction;
        public int WaitTime;
        public int WaitLeft;
        public string Key;
    }


    private static Dictionary<string, WaitCall> _waitCalls = new Dictionary<string, WaitCall>();
    private static Dictionary<string, IEnumerator> _waitCoroutines = new Dictionary<string, IEnumerator>();

    public static void WaitAndCall(string key, int wait, Action callback, Action everySecondAction = null)
    {
        if (_waitCalls.ContainsKey(key)) RemoveWaiter(key);
        if (_waitCoroutines.ContainsKey(key)) _waitCoroutines.Remove(key);

        var routine = StartTimer(key);
        _waitCoroutines.Add(key, routine);
        _waitCalls.Add(key, new WaitCall { Key = key, WaitTime = wait, Callback = callback, EverySecondAction = everySecondAction});

        CoroutineManager.StartCoroutineStatic(routine);
    }


    public static void RemoveWaiter(string key)
    {
        CoroutineManager.StopCoroutineStatic(_waitCoroutines[key]);
        if (_waitCalls.ContainsKey(key)) _waitCalls.Remove(key);
        if (_waitCoroutines.ContainsKey(key)) _waitCoroutines.Remove(key);
    }

    private static IEnumerator StartTimer(string key)
    {
        while (_waitCalls[key].WaitLeft != _waitCalls[key].WaitTime)
        {
            yield return new WaitForSeconds(1f);
            _waitCalls[key].WaitLeft++;
            _waitCalls[key].EverySecondAction?.Invoke();
        }

        _waitCalls[key].Callback?.Invoke();
    }
}
