using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign : MonoBehaviour
{
    [SerializeField] GameObject mapEdge;
    [SerializeField] GameObject mapDoor;
    [SerializeField] List<GameObject> mapPrefabs;

    [SerializeField] GameObject nextStageArea;

    public void SelectedMap(int minMapNum, int maxMapNum)
    {
        Instantiate(mapPrefabs[Random.Range(minMapNum, maxMapNum)]);
    }
    public void DrawMap()
    {
        Instantiate(mapEdge);
        Instantiate(mapDoor);
        for (int i = 1; i < 24; i+=3)
        {
            SelectedMap(i - 1, i + 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {

        }
    }
}
