using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameData
{
   private static int _currency = 0;
    private static int _snacks = 0;

    //Static Constructor to load data from playerPrefs
    static GameData ()
    {
        _currency = PlayerPrefs.GetInt ( "Currency", 0 );
        _snacks = PlayerPrefs.GetInt ( "Snacks", 0 );
    }

    public static int Currency
    {
        get { return _currency; }
        set { PlayerPrefs.SetInt ("Currency", (_currency = value) ); }
    }
    public static int Snacks
    {
        get { return _snacks; }
        set { PlayerPrefs.SetInt("Snacks", (_snacks = value)); }
    }
}

