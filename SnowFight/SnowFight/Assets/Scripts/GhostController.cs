using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
	private int health = 100;
	GameManager gmr;

    void Awake()
    {
        gmr = FindObjectOfType<GameManager>();
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

	public void DecrementHealth(int h){
		health -= h;
		if (gmr) {
			gmr.hpText[1].text = "Enemy: " + health;
		}
		if (health <= 0) {
			GameOver ();
		}
	}

	public void GameOver(){

	}
}
