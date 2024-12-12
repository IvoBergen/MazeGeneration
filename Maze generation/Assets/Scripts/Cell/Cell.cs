using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;
    [SerializeField] private GameObject _unvisitedBlock;
    
    private Dictionary<Vector3, GameObject> _walls;
    public bool IsVisited { get; private set; }
    
    /// <summary>
    /// Sets the dictionary
    /// </summary>
    private void Awake()
    {
        _walls = new Dictionary<Vector3, GameObject>
        {
            { Vector3.left, _leftWall },
            { Vector3.right, _rightWall },
            { Vector3.forward, _frontWall },
            { Vector3.back, _backWall }
        };
    }
    
    /// <summary>
    /// Sets the cell to visited when the algorithm passes over it
    /// </summary>
    /// <param name="visited">A bool to show if a cell is visited</param>
    private void SetVisited(bool visited)
    {
        IsVisited = visited;
        _unvisitedBlock.SetActive(!visited);
    }
    
    /// <summary>
    /// Separate function to set the state for visited cells during the coroutine 
    /// </summary>
    public void Visit()
    {
        SetVisited(true);
    }

    /// <summary>
    /// It destroys the wall based on the direction while using the dictionary for the walls
    /// </summary>
    /// <param name="direction">the calculated direction</param>
    public void ClearWall(Vector3 direction)
    {
        direction = direction.normalized; // Ensure direction is normalized
        if (_walls.TryGetValue(direction, out var wall))
        {
            wall.SetActive(false);
        }
    }
    
    /// <summary>
    /// sets the walls 
    /// </summary>
    /// <param name="leftWall"></param>
    /// <param name="rightWall"></param>
    /// <param name="frontWall"></param>
    /// <param name="backWall"></param>
    public void InitializeWalls(GameObject leftWall, GameObject rightWall, GameObject frontWall, GameObject backWall)
    {
        _leftWall = leftWall;
        _rightWall = rightWall;
        _frontWall = frontWall;
        _backWall = backWall;

        Awake();
    }
}