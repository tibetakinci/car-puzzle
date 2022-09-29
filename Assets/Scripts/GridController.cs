using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject ParentGrid;
    private Queue<GameObject> LeftCarGrids;
    private Queue<GameObject> RightCarGrids;
    private List<GameObject> parkedGrids;

    // Start is called before the first frame update
    void Start()
    {
        parkedGrids = new List<GameObject>();
        LeftCarGrids = new Queue<GameObject>();
        RightCarGrids = new Queue<GameObject>();
        SetLeftGrids();
        SetRightGrids();
    }

    private void SetRightGrids()
    {
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(2).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(3).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(4).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(5).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(0).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(1).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(7).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(8).gameObject);
        RightCarGrids.Enqueue(ParentGrid.transform.GetChild(6).gameObject);
        UnityEngine.Debug.Log(RightCarGrids.Peek().gameObject.tag);
    }

    private void SetLeftGrids()
    {
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(0).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(1).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(2).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(3).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(7).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(8).gameObject);
        LeftCarGrids.Enqueue(ParentGrid.transform.GetChild(6).gameObject);
        UnityEngine.Debug.Log(LeftCarGrids.Peek().gameObject.tag);
    }

    public GameObject GetRightGrid()
    {
        GameObject grid;
        do {
            grid = RightCarGrids.Dequeue();
            UnityEngine.Debug.Log(grid);
        } while(CheckParkedGrids(grid));
        parkedGrids.Add(grid);
        return grid;
    }

    public GameObject GetLeftGrid()
    {
        GameObject grid;
        do {
            grid = LeftCarGrids.Dequeue();
        } while(CheckParkedGrids(grid));
        parkedGrids.Add(grid);
        return grid;
    }

    private bool CheckParkedGrids(GameObject grid)
    {
        UnityEngine.Debug.Log(parkedGrids.Contains(grid));
        return parkedGrids.Contains(grid);
    }

    public bool CheckPark(GameObject car, GameObject grid)
    {
        return (car.gameObject.tag == "LeftCar" && grid.gameObject.tag == "LeftGrid") || (car.gameObject.tag == "RightCar" && grid.gameObject.tag == "RightGrid");
    }
}
