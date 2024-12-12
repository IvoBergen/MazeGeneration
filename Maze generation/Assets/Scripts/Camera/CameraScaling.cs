using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    /// <summary>
    /// Zooms the camera out based on the width, depth and cell size and that calculated 
    /// </summary>
    /// <param name="camera">The camera object</param>
    /// <param name="mapWidth">The width of the map</param>
    /// <param name="mapDepth">The depth of the map</param>
    /// <param name="cellSize">The size of the cell</param>
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
