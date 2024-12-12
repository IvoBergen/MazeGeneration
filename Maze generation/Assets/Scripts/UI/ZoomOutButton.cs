using UnityEngine;

public class ZoomOutButton : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Camera _mainCamera;
    
    [Header("Script Specific file")]
    [SerializeField] private CameraScaling _scaling;
    [SerializeField] private MazeGenerator _mazeSize;
    
    /// <summary>
    /// The function that is called while in play mode for the camera to zoom out
    /// </summary>
    public void ZoomOutCamera()
    {
        _scaling.AdjustCamera(_mainCamera, _mazeSize.mazeWidth, _mazeSize.mazeDepth,1.2f);
    }
}
