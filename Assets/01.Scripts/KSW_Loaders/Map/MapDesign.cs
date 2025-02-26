using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign : MonoBehaviour
{
    [SerializeField] GameObject mapEdge;
    [SerializeField] List<GameObject> mapPrefabs;

    public void SelectedMap(int minMapNum, int maxMapNum)
    {
        Instantiate(mapPrefabs[Random.Range(minMapNum, maxMapNum)]);
    }
    public void DrawMap()
    {
        Instantiate(mapEdge);
        for (int i = 1; i < 24; i+=3)
        {
            SelectedMap(i - 1, i + 2);
        }
    }
}
