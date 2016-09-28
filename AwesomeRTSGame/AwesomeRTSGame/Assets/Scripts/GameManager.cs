﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private UnitProperty m_SelectedUnit;
    public float m_SpawnWait = 2;
    private bool m_Spawning = true;

    public Transform m_LeftLowerCorner;
    public Transform m_RightUpperCorner;


	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
	}

    public Vector3 GenerateSpawnPosition()
    {
        Vector3 pos = new Vector3(Random.Range(m_LeftLowerCorner.position.x, m_RightUpperCorner.position.x),
                                    0.5f,
                                    Random.Range(m_LeftLowerCorner.position.y, m_RightUpperCorner.position.y));
        Collider[] hitColliders = Physics.OverlapSphere(pos, 3.0f);
        /*
        if (hitColliders.Length != 0)
        {
            return GenerateSpawnPosition();
        }
        */
        return pos;
    }
        

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(m_SpawnWait);
        while (m_Spawning)
        {
            Vector3 pos = GenerateSpawnPosition();
            CoinProperty.Create(this, pos);
            yield return new WaitForSeconds(m_SpawnWait);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    public void SelectUnit(UnitProperty unit)
    {
            m_SelectedUnit = unit;
    }

    public void MoveUnit(Vector3 position)
    {
        if (m_SelectedUnit)
        {
            m_SelectedUnit.SetTarget(position);
        }
    }
    
    public void AttackUnit(UnitProperty unit)
    {
        if (unit.m_Team != m_SelectedUnit.m_Team)
        {
            unit.TakeAttack(m_SelectedUnit);
        }
    }
}
