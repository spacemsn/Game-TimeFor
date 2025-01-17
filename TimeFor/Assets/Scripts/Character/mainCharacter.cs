using System;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainCharacter : MonoCache
{
    [Header("EntryPoint")]
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("����������")]
    public SaveData saveData;

    [Header("����������")]
    public attackCharacter attack;
    public indicatorCharacter indicators;
    public artifactCharacter artifacts;
    public moveCharacter movement;
    public bookCharacter book;
    public DialogManager dialogManager;

    private void Start()
    {
        movement = this.GetComponent<moveCharacter>();
        attack = this.GetComponent<attackCharacter>();
        indicators = this.GetComponent<indicatorCharacter>();
        artifacts = this.GetComponent<artifactCharacter>();
        dialogManager = this.GetComponent<DialogManager>();
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        book = player.book;
    }
}
