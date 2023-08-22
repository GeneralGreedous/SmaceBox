using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameButtons : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.UpdateGameState(GameState.NewGame);
    }

    public void LoadGame()
    {

    }

    public void Settings()
    {

    }

    public void Exit()
    {

    }
}
