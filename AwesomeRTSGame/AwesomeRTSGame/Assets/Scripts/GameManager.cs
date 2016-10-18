using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private UnitProperty m_SelectedUnit;
    public float m_SpawnWait = 2;
    private bool m_Spawning = true;

    public Transform m_LeftLowerCorner;
    public Transform m_RightUpperCorner;
    public Transform m_PlayerSpawningPoint;
    public Transform m_EnemySpawningPoint;
    public int[] m_Coin;
    private int[] m_Cost;
    public Text m_CoinText;
    public Text m_EndGameText;
    public const int playerTeam = 0;
    

    private string[] m_UnitTypes;

	// Use this for initialization
	void Start () {
        m_Coin = new int[2] { 1, 1 };
        m_Cost = new int[3] { 1, 2, 4 };
         m_UnitTypes = new string[3] { "Bot", "OpossumUnit", "CrowUnit" };
        StartCoroutine(SpawnWaves());
	}

    public Vector3 GenerateSpawnPosition()
    {
        Vector3 pos = new Vector3(Random.Range(m_LeftLowerCorner.position.x, m_RightUpperCorner.position.x),
                                    0.5f,
                                    Random.Range(m_LeftLowerCorner.position.y, m_RightUpperCorner.position.y));
        //Collider[] hitColliders = Physics.OverlapSphere(pos, 3.0f);
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
	
    public UnitProperty SpawnUnit(int type, int team)
    {
        if (m_Coin[team] >= m_Cost[type])
        {
            AddCoin(-m_Cost[type], team);
            if (team == 0)
            {
                return UnitProperty.Create(this, m_UnitTypes[type], m_PlayerSpawningPoint.position, 0);
            }
            else
            {
                return UnitProperty.Create(this, m_UnitTypes[type], m_EnemySpawningPoint.position, 1);
            }
        }
        return null;
        
    }

    public void SpawnUnit(int type, int team, int cost)
    {
        SpawnUnit(type, team);
        m_Coin[team] -= cost;
    }

    public void SelectUnit(UnitProperty unit)
    {
        if (m_SelectedUnit != null) {
            m_SelectedUnit.SetSelected(false);
        }

        m_SelectedUnit = unit;
        if (unit != null) {
            m_SelectedUnit.SetSelected(true);
        }
    }

    public void SetUnitInteractibleTarget(UnitInteractible interactible)
    {
        if (m_SelectedUnit) {
            m_SelectedUnit.SetInteractibleTarget(interactible);
        }
    }

    public void MoveUnit(Vector3 position)
    {
        if (m_SelectedUnit) {
            m_SelectedUnit.SetPositionTarget(position);
        }
    }
    
    public void AttackUnit(UnitProperty unit)
    {
        if (unit && unit.m_Team != m_SelectedUnit.m_Team) {
            unit.TakeAttack(m_SelectedUnit);
        }
    }

    public void AttackBase(BaseProperty mbase)
    {
        if (mbase.m_Team != m_SelectedUnit.m_Team)
        {
            mbase.TakeDamage(m_SelectedUnit.m_AttackPower);
        }
    }

    public void AddCoin(int value, int team)
    {
        m_Coin[team] += value;
        if (team == 0) {
            m_CoinText.text = "Coin: " + m_Coin[0];
        }

    }
    public void EndGame(int losingTeam)
    {
        if (losingTeam == 1)
        {
            m_EndGameText.text = "You are victorious";
        }else
        {
            m_EndGameText.text = "You are defeated";
        }
    }
}
