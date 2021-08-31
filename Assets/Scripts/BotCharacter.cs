using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacter : MonoBehaviour
{
    BotCharacter[] bots;

    public BotCharacter botPrefab;

    public float runSpeed;

    //飞溅
    public void GroupBlowout()
    {

    }

    // 生成怪物
    public void SpawnBot()
	{

	}

    // 收到伤害
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
