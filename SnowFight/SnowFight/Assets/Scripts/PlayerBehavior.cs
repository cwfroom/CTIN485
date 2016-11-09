using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
    GameManager gmr;
    private bool online = false;
    public int playerID = 0;

    private CharacterController controller;
   
	// Use this for initialization
	void Start () {
        gmr = FindObjectOfType<GameManager>();
        if (gmr)
        {
            online = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
    
	}
}
