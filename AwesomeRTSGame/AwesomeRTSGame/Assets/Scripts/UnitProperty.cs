using UnityEngine;
using System.Collections;

public class UnitProperty : MonoBehaviour {
    public int m_Team;
    public int m_Health = 100;
    public int m_AttackRange = 5;

    private NavMeshAgent        m_NavAgent;
    private Vector3             m_PositionalTarget;
    private UnitInteractible m_InteractibleTarget;

    private AnimationStateController m_AnimationStateController;

    void Start () 
    {
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_PositionalTarget = transform.position;
        m_AnimationStateController = GetComponentInChildren<AnimationStateController>();
    }
	
    public static UnitProperty Create(GameManager gmr, string type, Vector3 initialPos, int team)
    {
        string path = "Prefabs/" + type;
        GameObject nUnit = Instantiate(Resources.Load(path)) as GameObject;
        nUnit.transform.position = initialPos;

        UnitProperty nUP = nUnit.GetComponent<UnitProperty>();
        nUP.m_Team = team;

        return nUP; 
    } 

	void Update () 
    {
        if (m_InteractibleTarget != null) {
            m_NavAgent.SetDestination(m_InteractibleTarget.transform.position);
            if (Vector3.Distance(m_InteractibleTarget.transform.position, this.transform.position) < 0.5f) {
                m_InteractibleTarget.UnitInteract(this);
            }
        }
	    else if (Vector3.Distance(transform.position, m_PositionalTarget) > 0.1f) {
            m_NavAgent.SetDestination(m_PositionalTarget);
        }
	}

    public void SetPositionTarget(Vector3 posTargetIn)
    {
        m_InteractibleTarget = null;
        m_PositionalTarget = posTargetIn;
        m_AnimationStateController.UpdateAnimationState(AnimationState.Walking);
    }

    public void SetInteractibleTarget(UnitInteractible interactibleTargetIn)
    {
        m_PositionalTarget = interactibleTargetIn.transform.position;
        m_InteractibleTarget = interactibleTargetIn;
    }

    public void TakeAttack(UnitProperty attacker)
    {
        if ((attacker.transform.position - this.transform.position).magnitude <= attacker.m_AttackRange) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
