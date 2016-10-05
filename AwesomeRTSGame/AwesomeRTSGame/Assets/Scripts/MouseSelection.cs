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
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.GetComponent<UnitProperty>()) {
                    m_Gmr.SelectUnit(hitInfo.transform.GetComponent<UnitProperty>());
                }
                else {
                    //m_Gmr.SpawnUnit(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_CameraDepth));
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
                else if (hitInfo.transform.GetComponent<UnitProperty>()) {
                    m_Gmr.AttackUnit(hitInfo.transform.GetComponent<UnitProperty>());
                }
                else {
                    m_Gmr.MoveUnit(hitInfo.point);
                }
            }
        }
    }
}
