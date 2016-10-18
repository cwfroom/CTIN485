using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    private GameManager m_Gmr;
    private List<UnitProperty> m_units;
    private bool bhasUnitCollectCoin;

	// Use this for initialization
	void Start () {
        m_Gmr = GetComponent<GameManager>();
        m_units = new List<UnitProperty>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (m_Gmr.m_Coin[1] >= 1)
        {
            m_units.Add(m_Gmr.SpawnUnit(0, 1));
        }
        if (FindCoin())
        {
            m_units[0].SetPositionTarget(FindCoin().transform.position);

            if (m_units.Count > 1)
            {
                float dist = Vector3.Distance(m_units[0].transform.position, FindCoin().transform.position);

                int index = 0;
                for (int i=1; i < m_units.Count; i++)
                { 
                    if (Vector3.Distance(m_units[i].transform.position, FindCoin().transform.position) < dist)
                    {
                        dist = Vector3.Distance(m_units[i].transform.position, FindCoin().transform.position);
                        index = i;
                    }
                    
                }
                m_units[index].SetPositionTarget(FindCoin().transform.position);



            }


        }
	}


    private CoinProperty FindCoin()
    {
        return FindObjectOfType<CoinProperty>();
    }
}
