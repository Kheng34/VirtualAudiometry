using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneButtonsManager : MonoBehaviour
{
    public InputActionReference TriggerAction;
    private bool PrevSceneHovering, NextSceneHovering, TriggerReleased = true;
    private int CurrScnPtr = 0;

    void OnEnable() { TriggerAction.action.Enable(); }
    void OnDisable() { TriggerAction.action.Disable(); }
    void Start()
    {
        CurrScnPtr = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if ((0.1f < TriggerAction.action.ReadValue<float>()) && TriggerReleased)
        {
            TriggerReleased = false;
            if(PrevSceneHovering) 
            { 
                if(0 < CurrScnPtr) { CurrScnPtr--; SceneManager.LoadScene(CurrScnPtr, LoadSceneMode.Single); }
            }
            if (NextSceneHovering)
            {
                if(CurrScnPtr < ((SceneManager.sceneCountInBuildSettings) - 1)) { CurrScnPtr++; SceneManager.LoadScene(CurrScnPtr, LoadSceneMode.Single); }
            }
        }
        if (TriggerAction.action.ReadValue<float>() < 0.1f) { TriggerReleased = true; }
    }
    public void PrevSceneEntered() { PrevSceneHovering = true; }
    public void PrevSceneExited() { PrevSceneHovering = false; }
    public void NextSceneEntered() { NextSceneHovering = true; }
    public void NextSceneExited() { NextSceneHovering = false; }
}
