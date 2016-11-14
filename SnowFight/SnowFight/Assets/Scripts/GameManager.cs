using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    NetworkManager nm;
    int PlayerID;
    List<Vector3> SpawnPoints;
    PlayerBehavior player;
    GhostController ghost;

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
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (PlayerID == 0)
        {
            player.transform.position = SpawnPoints[0];
            ghost.transform.position = SpawnPoints[1];
        }else
        {
            player.transform.position = SpawnPoints[1];
            ghost.transform.position = SpawnPoints[0];
        }
    }

    public void SetPID(int id)
    {
        PlayerID = id;
    }

    public void AddSpawnPoint(Vector3 pos)
    {
        SpawnPoints.Add(pos);
    }

    public void SetPlayer(PlayerBehavior p)
    {
        player = p;
    }

    public void SetGhost(GhostController g)
    {
        ghost = g;
    }

    public void SendPos(Vector3 vec)
    {
        nm.SendVector("POS:" + PlayerID, vec);
    }

    public void ReceivePos(Vector3 vec){
        if (ghost)
        {
            ghost.transform.position = vec;
        }
    }

    

}
