using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    // Holders for building and nature objects, so they can be placed in right place from the script
    public Transform buildingHolder;
    [SerializeField] Building building;
    public Building Building
    {
        get
        {
            return building;
        }
        set
        {
            if (building == null)
            {
                building = value;
            }
        }
    }
    public Transform natureHolder;
    [SerializeField] GameObject natureObject;
    [Tooltip("Type of the tile, more info about tile types in GDD")]
    public TileType tileType;
    List<Tile> neighbours;

    public enum TileType
    {
        Regular,
        Forest,
        Rocks,
        Water
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Here Build Manager methods may be handled, ex. if the tile is clicked when the building mode is active -> build building here
        //BuildManager.instance.Build(this);
        BuildPanel.instance.gameObject.SetActive(true);
        BuildManager.instance.BuildModeOn = true;
        BuildManager.instance.SetBuildingToBuild(-1);
        BuildManager.instance.tileToBuildOn = this;
        //throw new System.NotImplementedException();
    }
}
