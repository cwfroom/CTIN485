using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {

    public float reachDistance;
    public GameObject defaultPointer;
    public GameObject selectPointer;
    public GameObject PickUpGesture;
    public GameObject CarryGesture;
	public GameObject Target;

  

    Vector3 center;
	Vector3 dir;
    public Vector3 carryObjectOffset;
    public Vector3 pickUpObjectOffset;
	
    public bool lerpTo;
    public bool lerpFrom;

    Transform item;
    Transform holder;
    Transform temp;

	public float force = 500f;
    float timeTakenDuringLerp = 0.01f;
    float timeStartedLerping;

	//private GameManager mGmr;

    void Awake () {
        reachDistance = 20.0f;
        center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
       

        lerpTo = false;
        lerpFrom = false;

        item = null;
        holder = null;
       
        defaultPointer.SetActive(true);
        selectPointer.SetActive(false);
    }

	void Start()
    {
		//mGmr = FindObjectOfType<GameManager> ();
	}
	
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

		if (item == null && Physics.Raycast(ray, out hit, reachDistance))
        {
            SetPointerType(hit.transform.gameObject.tag);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.tag == "Pickable" || hit.transform.gameObject.tag == "Holder"||hit.transform.gameObject.tag == "Carryable")
                
                {
                    PickUp(hit);
                }
                
            }
        }
        else
        {
            SetPointerType(null);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (item != null)
            {
                PutDown();
            }
		}

        PickUpLerp();
		

        Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.yellow);
    }

    void SetPointerType(string tag)
   
    {
      
       if(tag == "Pickable" || tag == "Carryable" || tag == "Movable" || tag == "Holder")
        

        {
            defaultPointer.SetActive(false);
            selectPointer.SetActive(true);
        }
        else
        {
            defaultPointer.SetActive(true);
            selectPointer.SetActive(false);
        }
    }

    void PickUp(RaycastHit hit)
    {
       
        if ((item == null && hit.transform.gameObject.tag == "Pickable" )||(item == null && hit.transform.gameObject.tag == "Carryable"))
            {
            
            item = hit.transform;
				if (item.GetComponent<Rigidbody> ()) {
					item.GetComponent<Rigidbody>().useGravity = false;
					item.GetComponent<Rigidbody>().isKinematic = true;
				}
				if (item.GetComponent<Collider> ()) {
					item.GetComponent<Collider>().isTrigger = true;
				}
				if (item.GetComponent<ItemType> ()) {
					item.GetComponent<ItemType>().onHand = true;
				}
            	if (item.GetComponent<MeshRenderer>())
            	{
                item.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            	}
            	item.transform.parent = transform;
				//item.transform.localScale = new Vector3(1,0.5f,1);
                lerpTo = true;
                timeStartedLerping = Time.time;
            if (item.gameObject.tag == "Pickable")
            {
                PickUpGesture.SetActive(true);
            }
            if (item.gameObject.tag == "Carryable")
            {
                CarryGesture.SetActive(true);
            }
           
        }
            else if (item != null && hit.transform.gameObject.tag == "Holder")
            {
               
                holder = hit.transform;
                item.transform.parent = holder.parent;
                lerpFrom = true;
                timeStartedLerping = Time.time;
            }
        
    }

    void PutDown()
    {
        // Drop object
        item.transform.parent = null;
		if (item.GetComponent<Rigidbody> ()) {
			item.GetComponent<Rigidbody>().useGravity = true;
			item.GetComponent<Rigidbody>().isKinematic = false;
		}
		if (item.GetComponent<Collider> ()) {
			item.GetComponent<Collider>().isTrigger = false;
		}
		if (item.GetComponent<Collider> ()) {
			item.GetComponent<Collider>().isTrigger = false;
		}
		if (item.GetComponent<ItemType> ()) { 
			item.GetComponent<ItemType>().onHand = false;
		}
        if (item.GetComponent<MeshRenderer>())
        {
            item.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        if (item.gameObject.tag == "Pickable")
        {
            PickUpGesture.SetActive(false);

			dir = Target.transform.position - transform.position;
			dir = dir.normalized;

			item.GetComponent<Rigidbody> ().AddForce (dir * force);	

        }
        if (item.gameObject.tag == "Carryable")
        {
            CarryGesture.SetActive(false);
        }
        item = null;
        lerpTo = false;
    }

    void PickUpLerp()
    {
        if (lerpTo)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            // Pick up object and hold it
            if (item.tag == "Carryable") { 
                item.position = Vector3.Lerp(item.position, transform.position + transform.forward * carryObjectOffset.z +
                                             transform.right * carryObjectOffset.x+ transform.up * carryObjectOffset.y, percentageComplete);
			
            }
            if (item.tag == "Pickable")
            {
                item.position = Vector3.Lerp(item.position, transform.position + transform.forward * pickUpObjectOffset.z +
                                             transform.right * pickUpObjectOffset.x + transform.up * pickUpObjectOffset.y, percentageComplete);
            }
            //item.rotation = Quaternion.Lerp(item.rotation, transform.rotation, percentageComplete);

            if ((item.position == transform.position + transform.forward * carryObjectOffset.z +
                transform.right * carryObjectOffset.x + transform.up * carryObjectOffset.y)||(item.position == transform.position + transform.forward * pickUpObjectOffset.z +
                transform.right * pickUpObjectOffset.x + transform.up * pickUpObjectOffset.y))
            {
                lerpTo = false;
            }

        }
        else if (lerpFrom)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            // Send object to holder
            item.position = Vector3.Lerp(item.position, holder.position, percentageComplete);
            item.rotation = Quaternion.Lerp(item.rotation, holder.rotation, percentageComplete);

            if (item.position == holder.position)
            {
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Collider>().isTrigger = false;
                item.GetComponent<ItemType>().onHand = false;
                Debug.Log("REACHED HERE");
                item = null;
                holder = null;
                lerpFrom = false;
            }
        }
    }


	
}
