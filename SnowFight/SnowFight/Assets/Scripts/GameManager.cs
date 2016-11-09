using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    NetworkManager nm;
    int PlayerID;
    List<Vector3> SpawnPoints;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        nm = GetComponent<NetworkManager>();
        SpawnPoints = new List<Vector3>();
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

    public void AddSpawnPoint(Vector3 pos)
    {
        SpawnPoints.Add(pos);
    }

}
