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
            gmr.SetPlayer(this);
        }
        pos = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (pos != transform.position)
        {

            pos = transform.position;
        }
	}
}
