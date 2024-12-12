using UnityEngine;
using UnityEngine.UI;

public class GenerateButton : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;

    //(Re)Generates the maze and starts the coroutine 
    public void RegenerateMaze()
    {
        _mazeGenerator.InitializeGrid();
        StartCoroutine(_mazeGenerator.GenerateMazeCoroutine(_mazeGenerator.mazeGrid[0, 0]));
    }
}
