using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
    GameManager gmr;
    private bool online = false;

    private Vector3 pos;

	// Use this for initialization
	void Start () {
        gmr = FindObjectOfType<GameManager>();
        if (gmr)
        {
            online = true;
            transform.position = gmr.SpawnPoints[gmr.PlayerID];
        }
        pos = transform.position;
    }

    
    // Update is called once per frame
    void Update () {
        if (pos != transform.position && online)
        {
            gmr.nm.SendPos(transform.position, transform.rotation);
            //gmr.SendPos(transform.position);
            pos = transform.position;
        }
	}
    
}
