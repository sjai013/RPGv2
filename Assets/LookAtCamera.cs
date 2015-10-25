using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour
{

    public Vector3 RotationOffset;

	
	// Update is called once per frame
	void Update ()
    {
	    transform.LookAt(Camera.main.transform);
	    transform.eulerAngles = transform.eulerAngles + RotationOffset;
    }
}
