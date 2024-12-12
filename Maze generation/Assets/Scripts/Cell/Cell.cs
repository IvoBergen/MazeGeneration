using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _frontWall;
    [SerializeField] private GameObject _backWall;
    [SerializeField] private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    private Dictionary<Vector3, GameObject> _walls;

    //Sets the dictionary
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

    //Makes sure that the code knows this is a visited cell
    private void SetVisited(bool visited)
    {
        IsVisited = visited;
        _unvisitedBlock.SetActive(!visited);
    }

    //Separate function to set the state for visited cells during the coroutine 
    public void Visit()
    {
        SetVisited(true);
    }

    public void ClearWall(Vector3 direction)
    {
        direction = direction.normalized; // Ensure direction is normalized.
        if (_walls.TryGetValue(direction, out var wall))
        {
            wall.SetActive(false);
        }
    }

    public void InitializeWalls(GameObject leftWall, GameObject rightWall, GameObject frontWall, GameObject backWall)
    {
        _leftWall = leftWall;
        _rightWall = rightWall;
        _frontWall = frontWall;
        _backWall = backWall;

        Awake(); // Reinitialize the dictionary after setting walls.
    }
}