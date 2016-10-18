using UnityEngine;
using System.Collections;

public class UIButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnRobot()
    {
        FindObjectOfType<GameManager>().SpawnUnit(0, 0);
    }

    public void SpawnOpossum()
    {
        FindObjectOfType<GameManager>().SpawnUnit(1, 0);
    }

    public void SpawnCrow()
    {
        FindObjectOfType<GameManager>().SpawnUnit(2, 0);
    }
}
