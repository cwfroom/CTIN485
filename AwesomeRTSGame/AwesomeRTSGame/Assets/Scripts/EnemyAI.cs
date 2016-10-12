using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    private GameManager m_Gmr;
    private List<UnitProperty> m_units;
    
	// Use this for initialization
	void Start () {
        m_Gmr = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (m_Gmr.m_Coin[1] >= 1)
        {
            m_units.Add(m_Gmr.SpawnUnit(0, 1));
        }
        if (FindCoin())
        {
            foreach (UnitProperty unit in m_units)
            {
                unit.SetPositionTarget(FindCoin().transform.position);
            }
        }
	}


    private CoinProperty FindCoin()
    {
        return FindObjectOfType<CoinProperty>();
    }
}
