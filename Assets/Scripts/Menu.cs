using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("External Objects")]
    public PlayerController player;
    public VoiceModule voiceModule;

    [Header("Controls")]
    public Toggle mouseToggle;
    public Toggle voiceToggle;

    public Button startGame;

    private void Awake()
    {
        OnToggleSomething();
        mouseToggle.onValueChanged.AddListener((b) => OnToggleSomething());
        voiceToggle.onValueChanged.AddListener((b) => OnToggleSomething());

        startGame.onClick.AddListener(() => StartGame());
    }

    void OnToggleSomething()
    {
        startGame.interactable = mouseToggle.isOn || voiceToggle.isOn;
    }

    void StartGame()
    {
        if (mouseToggle.isOn)
        {
            player.enabled = true;
        }
        if(voiceToggle.isOn)
        {
            voiceModule.enabled = true;
        }
        Destroy(gameObject);
    }
}
