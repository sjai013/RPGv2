using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour
{

    public float Time = 0f;

	void Start ()
	{
	    Destroy(this.gameObject,Time);
	}
	
}
