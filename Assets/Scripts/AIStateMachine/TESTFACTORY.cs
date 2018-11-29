using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTFACTORY : MonoBehaviour {
    public GameObject NoisePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject newNoise = Instantiate(NoisePrefab, new Vector3(70, 70, 0), Quaternion.identity);
            newNoise.GetComponent<EnemyNoise>().Init();

        }

    }
}
