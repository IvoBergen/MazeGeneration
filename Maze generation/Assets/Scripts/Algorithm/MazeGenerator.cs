using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [Header("Class specific Scripts")]
    [SerializeField] private Cell _mazeCellPrefab;
    public Cell[,] mazeGrid;

    [Header("Properties")]
    public int mazeWidth;
    public int mazeDepth; 
    private GameObject _container;
    
    /// <summary>
    /// creates the grid of the maze and Instantiates the cubes based on the selected scale
    /// </summary>
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

    
    /// <summary>
    /// destroys the container and grid when the maze is regenerated
    /// </summary>
    private void DestroyGrid()
    {
        if (_container == null)
            return;
        Destroy(_container);
    }
    
    
    /// <summary>
    /// A IEnumerator that generates destroys the walls between cubes to generate a maze
    /// </summary>
    /// <param name="startCell"> the starting cell</param>
    /// <returns>A generated maze</returns>
    public IEnumerator GenerateMazeCoroutine(Cell startCell)
    {
        var cellStack = new Stack<Cell>();
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

    /// <summary>
    /// give the algorithm the next cell to visit based on the position of the current cell
    /// </summary>
    /// <param name="currentCell">The current cell the algorithm is on</param>
    /// <returns>The next unvisited cell</returns>
    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell).ToList();

        if (unvisitedCells.Count == 0) return null;

        var randomIndex = Random.Range(0, unvisitedCells.Count);
        return unvisitedCells[randomIndex];
    }

    
    /// <summary>
    /// calculates what cell is and isn't visited around the currentCell
    /// </summary>
    /// <param name="currentCell">The current cell the algorithm is on</param>
    /// <returns>A new cell for the algorithm to visit</returns>
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
    
    /// <summary>
    /// Destroys the walls based on the movement of the algorithm
    /// </summary>
    /// <param name="previousCell">The cell the algorithm was on before moving</param>
    /// <param name="currentCell">The cell the algorithm is on</param>
    private static void ClearWalls(Cell previousCell, Cell currentCell)
    {

        if (previousCell == null)return;
        
        var direction = currentCell.transform.position - previousCell.transform.position;
        
        previousCell.ClearWall(direction);
        currentCell.ClearWall(-direction);
        
    }
    
    /// <summary>
    /// Clears a specific wall with the help of the position and the movement direction
    /// </summary>
    /// <param name="cell">The cell from the wall that needs to be removed</param>
    /// <param name="direction">The direction to calculate which wall needs to be destroyed</param>
    private static void ClearWallsAtPosition(Cell cell, Vector3 direction)
    {
        cell.ClearWall(direction);
    }
}