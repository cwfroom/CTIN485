using UnityEngine;
using System.Collections;

public class PhotonNetworkManager : MonoBehaviour {
    private GameManager gmr;
    private static PhotonView ScenePhotonView;


    public enum NetworkStates
    {
        Idle = 0,
        Room = 1,
        InGame = 2,
        EndBattle = 3
    }

    public static NetworkStates currentState;


    // Use this for initialization
    void Start () {
        gmr = GetComponent<GameManager>();
        ScenePhotonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartMatching()
    {
        if (currentState == NetworkStates.Idle || currentState == NetworkStates.Room || currentState == NetworkStates.EndBattle)
        {

            if (currentState == NetworkStates.EndBattle || currentState == NetworkStates.Room)
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();

            }
            if (currentState == NetworkStates.Idle || currentState == NetworkStates.EndBattle)
            {
                
                //GetComponent<BackToScreen>().GoToMatchingRoom();
            }
            //join a random room
            currentState = NetworkStates.Room;
            GetComponent<Random_Matchmaker>().Connect();
        }
    }

    public void OnJoinedRoom()
    {

        if (PhotonNetwork.isMasterClient)
        {
            //networkPlayerID = PhotonNetwork.player.ID;
           // gamePlayerID = 0;
        }

        Debug.Log("Network ID: " + PhotonNetwork.player.ID);

    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);

        if (PhotonNetwork.isMasterClient)
        {
            if (PhotonNetwork.playerList.Length >= 2)
            {
                ScenePhotonView.RPC("LoadLevel", PhotonTargets.All);
            }

        }
    }





}
