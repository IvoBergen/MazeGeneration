using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateButton : MonoBehaviour
{
    [SerializeField] private Button _generateButton;
    [SerializeField] private MazeGenerator _mazeGenerator;

    public void RegenerateMaze()
    {
        _mazeGenerator.InitializeGrid();
        //_mazeGenerator.GenerateMaze(null, _mazeGenerator.mazeGrid[0,0]);
        StartCoroutine(_mazeGenerator.GenerateMazeCoroutine(_mazeGenerator.mazeGrid[0, 0]));
    }
}
