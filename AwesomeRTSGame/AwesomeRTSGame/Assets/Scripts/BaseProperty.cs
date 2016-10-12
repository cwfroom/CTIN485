using UnityEngine;
using System.Collections;

public class BaseProperty : MonoBehaviour {

    public int m_Health = 200;
    public int m_Team = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            FindObjectOfType<GameManager>().EndGame(m_Team);
        }
    }
}
