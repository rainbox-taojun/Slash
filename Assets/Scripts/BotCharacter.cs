using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacter : MonoBehaviour
{
    [HideInInspector]
    public Bounds bounds;

	bool isAlive = true;
	Rigidbody rigid;
	const float yHeight = -0.55f;

	private void Awake()
	{
		bounds = GetComponent<Collider>().bounds;
		GetComponent<Collider>().enabled = false;
		rigid = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (isAlive)
		{
			var pos = transform.localPosition;
			pos.y = yHeight;
			transform.localPosition = pos;
		}
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
