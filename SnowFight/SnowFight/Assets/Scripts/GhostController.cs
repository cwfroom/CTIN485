using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
    void Awake()
    {
        GameManager gmr = FindObjectOfType<GameManager>();
        if (gmr)
        {
            gmr.SetGhost(this);
        }
    }


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
