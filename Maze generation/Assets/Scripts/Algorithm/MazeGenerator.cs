using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Cell _mazeCellPrefab;

    public int mazeWidth;

    public int mazeDepth; 

    public Cell[,] mazeGrid;

    private GameObject _container;

    // creates the grid of the maze and Instantiates the cubes based on the selected scale
    public void InitializeGrid()
    {
        DestroyGrid();
        
        mazeGrid = new Cell[mazeWidth, mazeDepth];

        _container = new GameObject("MazeContainer");
        
        for (var x = 0; x < mazeWidth; x++)
        {
            for (var z = 0; z < mazeDepth; z++)
            {
                mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                mazeGrid[x, z].transform.parent = _container.transform;
            }
        }
        
        ClearWallsAtPosition(mazeGrid[mazeWidth - 1, mazeDepth - 1], Vector3.right);
        ClearWallsAtPosition(mazeGrid[0, 0], Vector3.left);

    }

    //destroys the container and grid when the maze is regenerated
    private void DestroyGrid()
    {
        if (_container == null)
            return;
        Destroy(_container);
    }
    
    // A IEnumerator that generates destroys the walls between cubes to generate a maze
    public IEnumerator GenerateMazeCoroutine(Cell startCell)
    {
        Stack<Cell> cellStack = new Stack<Cell>();
        cellStack.Push(startCell);
        startCell.Visit();

        while (cellStack.Count > 0)
        {
            var currentCell = cellStack.Peek();
            var nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                ClearWalls(currentCell, nextCell);
                nextCell.Visit();
                cellStack.Push(nextCell);
            }
            else
            {
                cellStack.Pop();
            }
            
            if (cellStack.Count % 1000 == 0)
                yield return null;
        }
    }

    //give the algorithm the next cell to visit based on the position of the current cell
    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell).ToList();

        if (unvisitedCells.Count == 0) return null;

        var randomIndex = Random.Range(0, unvisitedCells.Count);
        return unvisitedCells[randomIndex];
    }

    //calculates what cell is and isn't visited around the currentcell
    private IEnumerable<Cell> GetUnvisitedCells(Cell currentCell)
    {
        var position = currentCell.transform.position;
        var x = (int)position.x;
        var z = (int)position.z;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, z];
            
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    //destroys the walls based on the movement of the algorithm
    private static void ClearWalls(Cell previousCell, Cell currentCell)
    {

        if (previousCell == null)return;
        
        var direction = currentCell.transform.position - previousCell.transform.position;
        
        previousCell.ClearWall(direction);
        currentCell.ClearWall(-direction);
        
    }
    
    //clears a specific wall with the help of the position and the movement direction
    private static void ClearWallsAtPosition(Cell cell, Vector3 direction)
    {
        cell.ClearWall(direction);
    }
    

}