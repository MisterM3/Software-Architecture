using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEditor;


public class GridSystemTests
{

    GameObject building;



    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return new WaitForSeconds(0.5f);
        building = GameObject.FindObjectOfType<PREFABS_FOR_TESTING>().prefabsDifferentTurrets[0];

        yield return new WaitForEndOfFrame();
    }




    [UnityTest]
    public IEnumerator THROW_ERROR_TRY_BUILD_MULTIPLE_BUILDINGS_SAME_SPOT()
    {

        GridPosition position = new GridPosition(0, 0);

        LogAssert.Expect(LogType.Error, "Already a building on this position!");

        GridSystem.Instance.TryAddBuildingAtGridPosition(building.GetComponent<IGridObject>(), position);

        yield return new WaitForEndOfFrame();

        GridSystem.Instance.TryAddBuildingAtGridPosition(building.GetComponent<IGridObject>(), position);
        yield return null;
    }

    [UnityTest]
    public IEnumerator REMOVE_BUILDING_FROM_GRID()
    {

        GridPosition position = new GridPosition(1, 1);

        GridSystem.Instance.AddBuildingAtGridPosition(building.GetComponent<IGridObject>(), position);

        yield return new WaitForEndOfFrame();

        GridSystem.Instance.RemoveGridObjectAtLocation(position);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CAN_REMOVE_BUILDING_AND_BUILD_ON_SPOT_AGAIN()
    {
        GridPosition position = new GridPosition(2, 2);

        GridSystem.Instance.AddBuildingAtGridPosition(building.GetComponent<IGridObject>(), position);

        yield return new WaitForEndOfFrame();

        GridSystem.Instance.RemoveGridObjectAtLocation(position);

        yield return new WaitForEndOfFrame();

        GridSystem.Instance.AddBuildingAtGridPosition(building.GetComponent<IGridObject>(), position);
        yield return null;

    }
    [UnityTest]
    public IEnumerator CAN_REMOVE_BUILDING_EVEN_IF_NOTHING_ON_IT()
    {
        GridPosition position = new GridPosition(3, 3);

        GridSystem.Instance.RemoveGridObjectAtLocation(position);

        return null;



    }
}