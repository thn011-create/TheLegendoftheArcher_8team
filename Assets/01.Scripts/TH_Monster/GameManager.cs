using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;
    private UIManager uiManager;
    public static bool isFirstLoading = true;

    public MapDesign design;

    [SerializeField] GameObject nextStageArea;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        _playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        
    }

    private void Start()
    {
        design = GetComponentInChildren<MapDesign>();
        if (!isFirstLoading)
        {
            design.DrawMap();
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }

    }

    public void StartGame()
    {
        //uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        //uiManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
        /*enemyManager.StopWave();
        Destroy(design.mapDoor);*/

    }

    public void GameOver()
    {
        enemyManager.StopWave();
        //uiManager.SetGameOver();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            StartGame();
        }
    }
}

