using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public string Name;
    public int AttackDamage;
    public int MaxHealth;
}

public class PlayerInfoLoader
{
    public PlayerInfo PlayerInfo { get; private set; }

    public PlayerInfoLoader(string path = "PlayerData")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        PlayerInfo = JsonUtility.FromJson<Wrapper>(jsonData).Player;
    }
    [Serializable]
    private class Wrapper
    {
        public PlayerInfo Player;
    }
}