using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public Vector3 GetLeftPointWithSpace(int percent)
    {
        var space =  _camera.pixelWidth / 100 * percent;
        var pixelPosition = 0 + space;

        return _camera.ScreenToWorldPoint(new Vector3(pixelPosition, 0, 0));
    }
    
    public Vector3 GetRightPointWithSpace(int percent)
    {
        var space =  _camera.pixelWidth / 100 * percent;
        var pixelPosition = _camera.pixelWidth - space;

        return _camera.ScreenToWorldPoint(new Vector3(pixelPosition, 0, 0));
    }
}
