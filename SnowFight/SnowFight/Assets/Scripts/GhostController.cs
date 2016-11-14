using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager gmr = FindObjectOfType<GameManager>();
        if (gmr)
        {
            gmr.SetGhost(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
