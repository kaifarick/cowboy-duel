using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashInitializer : MonoBehaviour
{
    [SerializeField] private SplashLoader splashLoader;
    [SerializeField] private CanvasGroup _canvasGroup;

    private const int MIN_LOAD_TIME = 1;
    private bool _minTimeLeft;
    private enum Scene 
    {
        Initialize = 0,
        Main = 1, 
    }


    private void Start()
    {
        Application.targetFrameRate = 60;
        
        StartCoroutine(Initializer());
        TimerManager.WaitAndCall("splashLoader", MIN_LOAD_TIME, () => _minTimeLeft = true);
    }


    private IEnumerator Initializer()
    {
        float updateTime = 0.2f;
        
        var load= SceneManager.LoadSceneAsync((int)Scene.Main, LoadSceneMode.Additive);
        
        while (!load.isDone || !_minTimeLeft)
        {
            yield return new WaitForSeconds(updateTime);
            //Debug.Log("progress" + load.progress);
            splashLoader.UpdateView(updateTime,load.progress);
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_canvasGroup.DOFade(0, 1));
        sequence.AppendCallback(() => SceneManager.UnloadSceneAsync((int) Scene.Initialize));

    }
    
}
