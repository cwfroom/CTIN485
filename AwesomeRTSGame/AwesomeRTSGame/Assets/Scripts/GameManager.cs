using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private UnitProperty m_SelectedUnit;


	// Use this for initialization
	void Start () {
	
	}

    public void SpawnUnit()
    {



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
