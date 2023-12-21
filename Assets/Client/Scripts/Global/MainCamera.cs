using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public Vector3 GetLeftPointWithSpace(int space)
    {
        var pixelPosition = 0 + space;

        return _camera.ScreenToWorldPoint(new Vector3(pixelPosition, 0, 0));
    }
    
    public Vector3 GetRightPointWithSpace(int space)
    {
        var pixelPosition = _camera.pixelWidth - space;

        return _camera.ScreenToWorldPoint(new Vector3(pixelPosition, 0, 0));
    }
}
