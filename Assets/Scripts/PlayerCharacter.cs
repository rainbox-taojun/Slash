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

    public float speed = 5f; // �ٶ�
    public const float speedMax = 10f; // ����ٶ�
    public const float speedAcc = 5f; // ���ٶ�

    public bool canAttack = true;
    const float attackTime = 0.3f;
    public float attackingTime;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();

        if (attackingTime > 0)
		{
            attackingTime = attackTime - Time.deltaTime;
		}
    }

    void Move()
	{
        Vector3 velocityTemp = rigid.velocity;
        speed += (speedAcc * Time.deltaTime); // ���ٶ� * deltaTime �õ�ÿ֡�ļ��ٶ�
        speed = Mathf.Clamp(speed, 0f, speedMax); // �������ֵ
        velocityTemp.z = speed;
        if (velocityTemp.y > 0f)
		{
            velocityTemp.y = 0f;
        }

        rigid.velocity = velocityTemp;
    }

    public void Attack()
	{
        if (!canAttack) return;

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

        attackingTime = attackTime;
        canAttack = false;

        CancelInvoke("ResetCanAttack");
        Invoke("ResetCanAttack", attackingTime + 1);

    }

    public void ResetCanAttack()
	{
        canAttack = true;
	}

    public void HitEnemy(GameObject target)
	{
        // ���е��˺�����ˢ�¹���״̬
        ResetCanAttack();
        //swordHitEffect.transform.position = target.transform.position;
        //swordHitEffect.Play();
        //GameMode.Scored();
    }
}
