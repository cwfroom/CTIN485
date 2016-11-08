using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    NetworkManager nm;
    int PlayerID;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        nm = GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLobby()
    {
        nm.Connect();
        SceneManager.LoadScene("Lobby");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void SetPID(int id)
    {
        PlayerID = id;
    }

    
}
