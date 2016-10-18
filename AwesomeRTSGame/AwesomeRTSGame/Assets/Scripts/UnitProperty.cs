using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitProperty : MonoBehaviour {
    public int m_Team;
    public int m_Health = 100;
    public int m_AttackPower = 20;
    public int m_AttackRange = 5;

    [SerializeField] private Material   m_outlineMaterial;
    [SerializeField] private Transform  m_MeshPivot;

    private NavMeshAgent        m_NavAgent;
    private Vector3             m_PositionalTarget;
    private UnitInteractible m_InteractibleTarget;
    private AnimationStateController m_AnimationStateController;
    private Renderer[]      m_renderers;


    void Start () 
    {
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_PositionalTarget = transform.position;
        m_AnimationStateController = GetComponentInChildren<AnimationStateController>();
        m_renderers = m_MeshPivot.GetComponentsInChildren<Renderer>();
    }

    public void SetSelected(bool selected)
    {
        if (selected) {
            bool hasOutline = false;

            foreach (Renderer renderer in m_renderers) {
                foreach (Material mat in renderer.materials) {
                    if (mat.shader == m_outlineMaterial.shader) {
                        hasOutline = true;
                    }
                }

                if (!hasOutline) {
                    List<Material> matList = renderer.materials.ToList<Material>();
                    matList.Add(m_outlineMaterial);
                    renderer.materials = matList.ToArray();
                }
            }
        }else {
            foreach (Renderer renderer in m_renderers) {
                List<Material> matList = renderer.materials.ToList<Material>();
                for (int i = 0; i < matList.Count; i++) {
                    if (matList[i].shader == m_outlineMaterial.shader) {
                        matList.RemoveAt(i);
                        i--;
                    }
                }
                renderer.materials = matList.ToArray();
            }
        }
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

        //Auto attack other untis
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
