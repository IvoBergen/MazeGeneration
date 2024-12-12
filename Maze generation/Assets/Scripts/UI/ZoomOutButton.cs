using UnityEngine;

public class ZoomOutButton : MonoBehaviour
{
    [SerializeField] private CameraScaling _scaling;
    [SerializeField] private MazeGenerator _mazeSize;
    [SerializeField] private Camera _mainCamera;


    //The function that is called while in play mode for the camera to zoom out
    public void ZoomOutCamera()
    {
        _scaling.AdjustCamera(_mainCamera, _mazeSize.mazeWidth, _mazeSize.mazeDepth,1.2f);
    }
}
