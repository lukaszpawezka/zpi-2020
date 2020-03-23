using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public List<Building> buildingPrefabs;

    Building buildingToBuild;
    void Awake()
    {
        instance = this;
    }

    public void SetBuildingToBuild(int i)
    {
        if (i >= 0 && i < buildingPrefabs.Count)
        {
            buildingToBuild = buildingPrefabs[i];
        }
        else
        {
            buildingToBuild = null;
        }
    }

    public void Build(Tile tile)
    {
        if (tile.Building == null && buildingToBuild != null)
        {
            Building newBuilding = Instantiate(buildingToBuild, tile.buildingHolder);
            tile.Building = newBuilding;
            newBuilding.tile = tile;
        }
    }
}
