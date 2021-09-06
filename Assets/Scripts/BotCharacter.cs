using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacter : MonoBehaviour
{
    [HideInInspector]
    public Bounds bounds;

	bool isAlive = true;
	Rigidbody rigid;

	private void Start()
	{
		bounds = GetComponent<Collider>().bounds;
		rigid = GetComponent<Rigidbody>();
	}


	public void Blowout(Vector3 blowout, Vector3 angularVelocity)
	{
		GetComponent<Animator>().SetTrigger("Collapse");
		transform.parent = null;
		isAlive = false;
		rigid.velocity = blowout;
		rigid.angularVelocity = angularVelocity;
		Destroy(gameObject, 3);
	}
}
