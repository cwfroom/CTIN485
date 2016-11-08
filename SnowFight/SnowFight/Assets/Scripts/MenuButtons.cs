using UnityEngine;
using System.Collections;


public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GotoLobby()
    {
        FindObjectOfType<GameManager>().LoadLobby();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
