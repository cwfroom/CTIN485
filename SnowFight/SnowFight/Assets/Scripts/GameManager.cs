using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //NetworkManager nm;
    public PhotonNetworkManager nm;
    public int PlayerID;
    public List<Vector3> SpawnPoints;
    public GhostController ghost;
	public Text[] hpText;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        //nm = GetComponent<NetworkManager>();
        nm = GetComponent<PhotonNetworkManager>();
        SpawnPoints = new List<Vector3>();
		hpText = new Text[2];
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

	public void SetText(Text t, int id){
		hpText [id] = t;
	}

	public void SendHealth(int h){
		PhotonNetworkManager.ScenePhotonView.RPC("ReceiveHealth", PhotonTargets.Others, h);
	}

	[PunRPC] public void ReceiveHealth(int h){
		hpText [0].text = "You: " + h;
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
