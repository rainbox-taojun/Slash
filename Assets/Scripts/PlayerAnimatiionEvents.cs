using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    PlayerCharacter character;

    void Start()
    {
        character = FindObjectOfType<PlayerCharacter>();
    }

    public void OnAnimationEvent_Attack()
	{
        // �������������Layer Mask��ֻ���ɱ�����������
        var colliders = Physics.OverlapSphere(transform.position, (1.5f));

        foreach(var collider in colliders)
        {
            var botGroup = collider.GetComponent<BotGroup>();
			if (botGroup)
			{
                character.HitEnemy(collider.gameObject);
                botGroup.TakeDamage();
                return;
			}
		}

    }
}
