using UnityEngine;
using System.Collections;

public class UnitProperty : MonoBehaviour {
    public int m_Team;
    public int m_Health = 100;
    public int m_AttackRange = 5;

    private NavMeshAgent m_NavAgent;
    private Vector3 m_Target;

    private AnimationStateController m_AnimationStateController;

    // Use this for initialization
    void Start () {
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Target = transform.position;
        m_AnimationStateController = GetComponentInChildren<AnimationStateController>();
    }
	
    public static UnitProperty Create(GameManager gmr, string type, Vector3 initialPos, int team){
        string path = "Prefab/" + type;
        GameObject nUnit = Instantiate(Resources.Load(path)) as GameObject;
        nUnit.transform.position = initialPos;

        UnitProperty nUP = nUnit.GetComponent<UnitProperty>();
        nUP.m_Team = team;

        return nUP; 
    } 

	// Update is called once per frame
	void Update () {
	    if (transform.position != m_Target)
        {
            m_NavAgent.SetDestination(m_Target);
        }
	}

    public void SetTarget(Vector3 target)
    {
        Debug.Log(target);
        m_Target = target;
        m_AnimationStateController.UpdateAnimationState(AnimationState.Walking);
    }

    public void TakeAttack(UnitProperty attacker)
    {
        if ((attacker.transform.position - this.transform.position).magnitude <= attacker.m_AttackRange)
        {
            TakeDamage(20);
        }

    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
