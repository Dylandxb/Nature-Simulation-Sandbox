using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    private bool pausedGame;
    public Event gamePause;
    public UnityEvent gamePauseTest;
    public UnityEvent gameResumeTest;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject waypointMarker;

    void Start()
    {
        controlsPanel.SetActive(false);
    }

    void Update()
    {
        PauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausedGame = !pausedGame;
            //Pauses game
            //Disables time
            //Enables panels
            if (pausedGame)
            {
                Time.timeScale = 0;                         //FixedUpdate never called
                gamePauseTest.Invoke();
                //PlayerController.instance.FreezePlayer();
                controlsPanel.SetActive(true);
                waypointMarker.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                gameResumeTest.Invoke();
                //PlayerController.instance.UnFreezePlayer();
                controlsPanel.SetActive(false);
                waypointMarker.SetActive(true);
            }
        }
    }

    //Unscaled time on objects always plays animations regardless of time scale
}
