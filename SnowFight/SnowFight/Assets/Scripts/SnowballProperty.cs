using UnityEngine;
using System.Collections;

public class SnowballProperty : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Destroy (this.gameObject);
		GhostController ghost = collision.transform.GetComponent<GhostController> ();
		if (ghost) {
			ghost.DecrementHealth (20);
		}
		/*
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2)
			audio.Play();
		*/
	}
}
