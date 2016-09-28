using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private UnitProperty m_SelectedUnit;

    private int currentTeam = 1;

	// Use this for initialization
	void Start () {
	
	}

    public void SpawnUnit(Vector3 SpawnPos)
    {
        if (currentTeam == 1)
        {
            UnitProperty newUnit = UnitProperty.Create(this, "BlueBot", SpawnPos, 0);
        }else
        {
            UnitProperty newUnit = UnitProperty.Create(this, "RedBot", SpawnPos, 1);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentTeam = 1;
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            currentTeam = 2;
        }

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
