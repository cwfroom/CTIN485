using UnityEngine;
using System.Collections;

public class MouseSelection : MonoBehaviour {
    private GameManager m_Gmr;
    public int m_CameraDepth = 12;

	// Use this for initialization
	void Start () {
        m_Gmr = GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        //Left click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.GetComponent<UnitProperty>())
                {
                    m_Gmr.SelectUnit(hitInfo.transform.GetComponent<UnitProperty>());

                }else
                {
                    m_Gmr.SelectUnit(null);
                }
            }
            else
            {
                m_Gmr.SelectUnit(null);
            }
        }else  if (Input.GetMouseButtonDown(1))
        {

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.GetComponent<UnitProperty>())
                {
                    m_Gmr.AttackUnit(hitInfo.transform.GetComponent<UnitProperty>());
                }
                else
                {
                    Vector3 worldCoord = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_CameraDepth));
                    m_Gmr.MoveUnit(worldCoord);
                }
            }
            else
            {
                Vector3 worldCoord = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_CameraDepth));
                m_Gmr.MoveUnit(worldCoord);
            }
        }
    }

}
