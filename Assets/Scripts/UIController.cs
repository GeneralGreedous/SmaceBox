using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController Instance;
    [SerializeField] private GameObject runGame;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("UIController already on scene");
        }
        Instance = this;
    }

    public void SetActiveRungame(bool active)
    {
        runGame.SetActive(active);
    }
}
