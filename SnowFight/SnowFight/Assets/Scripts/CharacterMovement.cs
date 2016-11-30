using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float inputDelay = 0.1f;
    public float forwardVelocity = 12.0f;
    public float rotateVelocity = 100.0f;

    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput;

    public Quaternion TargetRotation {
        get { return targetRotation; }
    }

	void Start () {
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        forwardInput = turnInput = 0.0f;
	}
	
	void Update () {
        GetInput();
        Turn();
	}

    void FixedUpdate() {
        Run();
    }

    void GetInput() {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Run() {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            rBody.velocity = transform.forward * forwardInput * forwardVelocity;
        }
        else
        {
            rBody.velocity = Vector3.zero;
        }
    }

    void Turn() {
        if (Mathf.Abs(turnInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }
}

