using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width,_height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles; // Dictionary to store the tiles

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid(){
        for(int x=0;x<_width;x++){
            for(int y=0;y<_height;y++){
                var spawnedTile=Instantiate(_tilePrefab,new Vector3(x,y),Quaternion.identity); // Instantiate the tile
                spawnedTile.name="Tile "+x+" "+y; // Set the name of the tile

                 var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0); // Check if the tile is offset is even or odd
                 spawnedTile.Init(isOffset); // Initialize the tile

                  _tiles[new Vector2(x, y)] = spawnedTile; // Add the tile to the dictionary
            }
        }
        _cam.transform.position=new Vector3((float)_width/2,(float)_height/2,-10); // Set the camera position to the center of the grid
    }
}
