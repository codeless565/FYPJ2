﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonExit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.tag == "Player")
        {
            CProgression.Instance.UpdateDungeonProgression();
            SceneManager.LoadScene("TownScene");
            MAudio.Instance.PlayBGM(MAudio.Instance.BGMClipList[1]);
        }
    }
}
