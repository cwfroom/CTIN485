using UnityEngine;
using System.Collections;

public class MouseSelection : MonoBehaviour 
{
    private GameManager m_Gmr;

	void Start () 
    {
        m_Gmr = GetComponent<GameManager>();
	}

    void Update()
    {
        //Left click
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                UnitProperty hitUnit = hitInfo.transform.GetComponent<UnitProperty>();
                if (hitUnit != null && hitUnit.m_Team == GameManager.playerTeam) {
                    m_Gmr.SelectUnit(hitUnit);
                }
                else {
                    m_Gmr.SelectUnit(null);
                }
            }
            else {
                m_Gmr.SelectUnit(null);
            }
        }
        else  if (Input.GetMouseButtonDown(1)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                UnitInteractible interactible = hitInfo.transform.gameObject.GetComponent<UnitInteractible>();
                if (interactible != null) {
                    m_Gmr.SetUnitInteractibleTarget(interactible);
                }
                m_Gmr.MoveUnit(hitInfo.point);

                /*
                else if (hitInfo.transform.GetComponent<UnitProperty>()) {
                    m_Gmr.AttackUnit(hitInfo.transform.GetComponent<UnitProperty>());
                }else if (hitInfo.transform.GetComponent<BaseProperty>())
                {
                    m_Gmr.AttackBase(hitInfo.transform.GetComponent<BaseProperty>());
                }else {
                  }
                */
            }
        }
    }
}
