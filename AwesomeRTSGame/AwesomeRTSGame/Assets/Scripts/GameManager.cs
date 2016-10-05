using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private UnitProperty m_SelectedUnit;
    public float m_SpawnWait = 2;
    private bool m_Spawning = true;

    public Transform m_LeftLowerCorner;
    public Transform m_RightUpperCorner;
    public Transform m_PlayerSpawningPoint;
    public Transform m_EnemySpawningPoint;
    private int[] m_Coin;
    public Text m_CoinText;
    public const int playerTeam = 0;
    
	// Use this for initialization
	void Start () {
        m_Coin = new int[2];
        m_Coin[0] = 0;
        m_Coin[1] = 0;
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
	
    public void SpawnUnit(string type, int team)
    {
        if (team == 0)
        {
            UnitProperty.Create(this, type, m_PlayerSpawningPoint.position, 0);
        }else
        {
            UnitProperty.Create(this, type, m_EnemySpawningPoint.position, 1);
        }



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
        if(m_SelectedUnit != null && unit != null) {
            if (unit.m_Team != m_SelectedUnit.m_Team) {
                unit.TakeAttack(m_SelectedUnit);
            }
        }
    }

    public void AddCoin(int value, int team)
    {
        m_Coin[team] += value;
        if (team == 0) {
            m_CoinText.text = "Coin: " + m_Coin[0];
        }

    }
}
