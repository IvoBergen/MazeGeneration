using System;
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

    public void InitializeGrid()
    {
        DestroyGrid();
        
        mazeGrid = new Cell[mazeWidth, mazeDepth];

        _container = new GameObject("MazeContainer");
        
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeDepth; z++)
            {
                mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                mazeGrid[x, z].transform.parent = _container.transform;
            }
        }
        
    }

    private void DestroyGrid()
    {
        if (_container == null)
            return;
        Destroy(_container);
    }

    public void GenerateMaze(Cell previousCell, Cell currentCell)
    {
        
        
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        Cell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<Cell> GetUnvisitedCells(Cell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

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

    private void ClearWalls(Cell previousCell, Cell currentCell)
    {

        if (previousCell == null)return;
        
        var direction = currentCell.transform.position - previousCell.transform.position;
        
        previousCell.ClearWall(direction);
        currentCell.ClearWall(-direction);
        
    }

}

