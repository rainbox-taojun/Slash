using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    public enum AttackMotion
	{
        Left,
        Right
	}

    AttackMotion attackMotion = AttackMotion.Left;

    public ParticleSystem swordEffectRight;
    public ParticleSystem swordEffectLeft;

    public float speed = 5f; // 速度
    public const float speedMax = 10f; // 最大速度
    public const float speedAcc = 5f; // 加速度

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
        speed += (speedAcc * Time.deltaTime); // 加速度 * deltaTime 得到每帧的加速度
        speed = Mathf.Clamp(speed, 0f, speedMax); // 限制最大值
        velocityTemp.z = speed;
        if (velocityTemp.y > 0f)
		{
            velocityTemp.y = 0f;
        }

        rigid.velocity = velocityTemp;
    }

    public void Attack()
	{
        if (attackMotion == AttackMotion.Left)
		{
            animator.SetTrigger("OnLeftAttack");
            swordEffectLeft.Play();
            attackMotion = AttackMotion.Right;
        }
		else
		{
            animator.SetTrigger("OnRightAttack");
            swordEffectRight.Play();
            attackMotion = AttackMotion.Left;
        }
	}
}
