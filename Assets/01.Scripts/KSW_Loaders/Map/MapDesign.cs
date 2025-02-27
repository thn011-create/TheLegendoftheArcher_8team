using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign : MonoBehaviour
{
    [SerializeField] GameObject mapEdge;
    [SerializeField] public GameObject mapDoor;
    [SerializeField] GameObject[] mapPrefabs;

    public List<GameObject> currentTiles = new List<GameObject>();

    private bool IsFirstGenerate = true;

    public void GenerateMap(bool IsBoss)
    {
        if (IsFirstGenerate)
        {
            IsFirstGenerate = false;
            DrawMap(IsBoss);
            return;
        }
        ClearMap();
        DrawMap(IsBoss);
    }

    public GameObject SelectedMap(int minMapNum, int maxMapNum)
    {
        return Instantiate(mapPrefabs[Random.Range(minMapNum, maxMapNum)]);
    }
    
    public void DrawMap(bool IsBoss)
    {
        if (!IsBoss)
        {
            for (int i = 1; i < 24; i += 3)
            {
                currentTiles.Add(SelectedMap(i - 1, i + 2));
            }
        }
        else currentTiles.Add(Instantiate(SelectedMap(24, 25)));
        currentTiles.Add(Instantiate(mapEdge));
        currentTiles.Add(Instantiate(mapDoor));
    }

    public void DoorOpen()
    {
        Destroy(currentTiles[currentTiles.Count - 1]);
    }

    private void ClearMap()
    {
        foreach (var tile in currentTiles)
        {
            Destroy(tile);
        }
        currentTiles.Clear();
    }    
}
