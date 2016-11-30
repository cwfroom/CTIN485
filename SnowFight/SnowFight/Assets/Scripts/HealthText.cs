using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {
	public int id;

	void Awake(){
		GameManager gmr = FindObjectOfType<GameManager> ();
		if (gmr) {
			gmr.SetText (GetComponent<Text> (), id);
		}
	}
}
