using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WindowsManager : MonoBehaviour
{

    private readonly Dictionary<string, BaseWindow> _windows = new Dictionary<string, BaseWindow>();

    [Inject] private readonly DiContainer _diContainer;

    private T GetWindow<T>() where T : BaseWindow
    {
        string windowName = typeof(T).Name.ToString();
        T window;
        if (_windows.ContainsKey(windowName))
        {
            window = _windows[windowName] as T;
            if (window != null)
            {
                return window;
            }
        }

        var windowPrefab = Resources.Load<T>("Windows/" + windowName);
        window = Instantiate(windowPrefab, gameObject.transform);
        _windows.Add(windowName, window);
        
        _diContainer.Inject(window);

        return window;
    }
    
    public T OpenWindow<T>() where T : BaseWindow
    {
        T window = GetWindow<T>();
        window.Show();
        return window;
    }
}