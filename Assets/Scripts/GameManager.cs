using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameState State { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("GameManager already on scene");
        }
        Instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.Run);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Run:
                StartGame();
                break;
            case GameState.NewGame:
                StartingGame();
                NewGame();
                break;
            case GameState.LoadGame:
                StartingGame();
                break;
            case GameState.SaveGame:
                break;
            case GameState.GamePlay:
                break;
            default:
                break;
        }
    }

    private void StartGame()
    {
        ShipController.Instance.gameObject.SetActive(false);
        UIController.Instance.SetActiveRungame(true);

    }

    private void StartingGame()
    {
        UIController.Instance.SetActiveRungame(false);
        ShipController.Instance.gameObject.SetActive(true);
        
    }

    private void NewGame()
    {
        GalaxyManager.Instance.StartCreatingGalaxy();
        ShipController.Instance.transform.position = Vector3.zero;
    }
}





public enum GameState
{
    Run,
    NewGame,
    LoadGame,
    SaveGame,
    GamePlay,

}