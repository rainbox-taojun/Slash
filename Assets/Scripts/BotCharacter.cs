using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacter : MonoBehaviour
{
    BotCharacter[] bots;

    public BotCharacter botPrefab;

    public float runSpeed;

    //�ɽ�
    public void GroupBlowout()
    {

    }

    // ���ɹ���
    public void SpawnBot()
	{

	}

    // �յ��˺�
    public void TakeDamage()
	{
        GroupBlowout();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.z = runSpeed * Time.deltaTime;

        transform.position = pos;
    }
}
