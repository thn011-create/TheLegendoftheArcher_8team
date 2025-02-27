using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;
    

    public int currentWaveIndex = 0;

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
        
        if (!isFirstLoading)
        {
            
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }

    }

    public void StartGame()
    {
        StartNextWave();
        uiManager.SetPlayGame();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        Debug.Log("Wave " + currentWaveIndex + " Started");
        design.GenerateMap();

        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        Debug.Log("Wave " + currentWaveIndex + " Ended");
        design.DoorOpen();
        enemyManager.StopWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            return;
        }
        else if (collision.tag == "Player")
        {
            collision.transform.position = Vector3.zero;
            StartNextWave();
        }
    }

}

