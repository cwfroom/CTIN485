using UnityEngine;
using System.Collections;

public class BaseProperty : MonoBehaviour {

    public int m_Health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
    }
}
