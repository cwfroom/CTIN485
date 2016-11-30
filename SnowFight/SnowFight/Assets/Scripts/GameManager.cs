using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //NetworkManager nm;
    PhotonNetworkManager nm;
    public int PlayerID;
    public List<Vector3> SpawnPoints;
    GhostController ghost;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        //nm = GetComponent<NetworkManager>();
        nm = GetComponent<PhotonNetworkManager>();
        SpawnPoints = new List<Vector3>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLobby()
    {
        //nm.Connect();
        nm.StartMatching();
        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
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


    public void SetGhost(GhostController g)
    {
        ghost = g;
    }

    public void SendPos(Vector3 vec, Quaternion rot)
    {

    }



    /*
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
    */
    

}
