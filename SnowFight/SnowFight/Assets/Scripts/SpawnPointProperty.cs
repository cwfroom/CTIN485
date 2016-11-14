﻿using UnityEngine;
using System.Collections;

public class SpawnPointProperty : MonoBehaviour {

    void Awake()
    {
        GameManager gmr = FindObjectOfType<GameManager>();
        if (gmr)
        {
            gmr.AddSpawnPoint(transform.position);
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
