using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase {
    public static AudioDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = new AudioDatabase();
            return instance;
        }
    }
    private static AudioDatabase instance;

    public Dictionary<string, AudioClip> BGMDictonary;
    Dictionary<string, AudioClip> FXDictonary;

    private AudioDatabase()
    {
        BGMDictonary = new Dictionary<string, AudioClip>();
        FXDictonary = new Dictionary<string, AudioClip>();

        BGMDictonary.Add("mainmenu", (AudioClip)Resources.Load("Audio/BGM/bensound-creativeminds"));
        BGMDictonary.Add("gamescene", (AudioClip)Resources.Load("Audio/BGM/gamescene"));
        BGMDictonary.Add("townscene", (AudioClip)Resources.Load("Audio/BGM/townscene"));

        FXDictonary.Add("normalpiano", (AudioClip)Resources.Load("Audio/FX/normalpiano"));
        FXDictonary.Add("specialpiano", (AudioClip)Resources.Load("Audio/FX/specialpiano"));
        FXDictonary.Add("chargedpiano", (AudioClip)Resources.Load("Audio/FX/chargedpiano"));
    }

    public AudioClip getBGMAudio(string _clipname)
    {
        if (!BGMDictonary.ContainsKey(_clipname))
            return null;

        return BGMDictonary[_clipname];
    }

    public AudioClip getFXAudio(string _clipname)
    {
        if (!FXDictonary.ContainsKey(_clipname))
            return null;

        return FXDictonary[_clipname];
    }

}
