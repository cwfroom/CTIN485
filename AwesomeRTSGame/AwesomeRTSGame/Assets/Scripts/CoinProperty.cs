using UnityEngine;
using System.Collections;

public class CoinProperty : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static CoinProperty Create(GameManager gmr, Vector3 initialPos)
    {
        GameObject nUnit = Instantiate(Resources.Load("Prefabs/Coin")) as GameObject;
        nUnit.transform.position = initialPos;

        CoinProperty nCP = nUnit.GetComponent<CoinProperty>();     
        return nCP;
    }
}
