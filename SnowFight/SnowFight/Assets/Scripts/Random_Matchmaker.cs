using UnityEngine;

public class Random_Matchmaker : Photon.PunBehaviour
{
    private PhotonView myPhotonView;
    public byte maxPlayer = 2;

    // Use this for initialization
    public void Start(){ }    
   

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = maxPlayer };

        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }
    

    public void OnGUI()
    {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        //GUILayout.Label(PhotonNetwork.networkingPeer.RoundTripTime.ToString() + "ms");

    }
}
