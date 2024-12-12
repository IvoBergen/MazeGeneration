using UnityEngine;
using UnityEngine.UI;

public class GenerateButton : MonoBehaviour
{
    [Header("Script Specific file")]
    [SerializeField] private MazeGenerator _mazeGenerator;
    
    /// <summary>
    /// (Re)Generates the maze and starts the coroutine 
    /// </summary>
    public void RegenerateMaze()
    {
        _mazeGenerator.InitializeGrid();
        StartCoroutine(_mazeGenerator.GenerateMazeCoroutine(_mazeGenerator.mazeGrid[0, 0]));
    }
}
