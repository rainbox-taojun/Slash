using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    public float speed = 5f;
    public const float speedMax = 10f;
    public const float speedAcc = 5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
	{
        Vector3 velocityTemp = rigid.velocity;
        speed += (speedAcc * Time.deltaTime);
        speed = Mathf.Clamp(speed, 0f, speedMax);
        velocityTemp.z = speed;
        if (velocityTemp.y > 0f)
		{
            velocityTemp.y = 0f;
        }

        rigid.velocity = velocityTemp;
    }

    void Attack()
	{

	}
}
