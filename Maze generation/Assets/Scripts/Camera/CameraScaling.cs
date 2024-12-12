using UnityEngine;

public class CameraScaling : MonoBehaviour
{

    //Scales the camera based on the width, height and cell size and calculates that
    public void AdjustCamera(Camera camera, int mapWidth, int mapDepth, float cellSize)
    {
        var mapWidthInUnits = mapWidth * cellSize;
        var mapDepthInUnits = mapDepth * cellSize;
        
        var largestDimension = Mathf.Max(mapWidthInUnits, mapDepthInUnits);
        
        camera.orthographic = true;
        camera.orthographicSize = largestDimension / 2;
        
        camera.transform.position = new Vector3(mapWidthInUnits / 2, 10, mapDepthInUnits / 2);
        camera.transform.rotation = Quaternion.Euler(90, 0, 0); 
    }

}
