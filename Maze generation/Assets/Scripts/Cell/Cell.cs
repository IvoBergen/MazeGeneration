using System.Collections;
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
    

    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }

    public void ClearWall(Vector3 direction)
    {
        if(direction.x > 0)ClearRightWall();
        else if(direction.x < 0)ClearLeftWall();
        else if(direction.z > 0)ClearFrontWall();
        else if(direction.z < 0)ClearBackWall();
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }
}

