using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(PlayerController))]
public class VoiceModule : MonoBehaviour
{
    KeywordRecognizer kr;
    PlayerController pc;

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }
    private void OnDisable()
    {
        if(kr != null)
        {
            kr.Stop();
        }
    }
    private void OnEnable()
    {
        kr = new KeywordRecognizer(new string[]{ "apple", "bottle", "cup", "box", "paper", "bio", "glass", "plastic", "down"});
        kr.OnPhraseRecognized += Recognize;
        kr.Start();       
    }
    private void Recognize(PhraseRecognizedEventArgs args)
    {
        Command(args.text);
    }
    public void Command(string command)
    {
        if(command == "down")
        {
            pc.ThrowBack();
        }
        foreach(var trash in FindObjectsOfType<TrashObject>())
        {
            if(trash.voiceName == command)
            {
                pc.Pickup(trash);
            }
        }
        foreach(var dumpster in FindObjectsOfType<Dumpster>())
        {
            if ((dumpster.dumpsterType == Dumpster.Type.Bio && command == "bio") ||
                (dumpster.dumpsterType == Dumpster.Type.Glass && command == "glass") ||
                (dumpster.dumpsterType == Dumpster.Type.Paper && command == "paper") ||
                (dumpster.dumpsterType == Dumpster.Type.Plastic && command == "plastic"))
            {
                pc.ThrowIntoDumpster(dumpster);
                break;
            }
        }
    }
}
