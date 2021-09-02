using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGroup : MonoBehaviour
{
    BotCharacter[] bots;

    public BotCharacter botPrefab;

    public float runSpeed;

    //飞溅
    public void GroupBlowout()
    {

    }

    // 生成怪物
    public void SpawnBot(int botNum)
    {
        bots = new BotCharacter[botNum];
        Vector3 position;
        for (int i = 0; i<botNum;i++)
		{
            position = transform.position;
            BotCharacter bot = Instantiate<BotCharacter>(botPrefab);
            bots[i] = bot;

            //根据怪物数决定分散范围，并限定在groupBox内

            Vector3 splat_range;
            // 根据怪物数量，计算怪物再X，Z轴上的分布范围
            splat_range.x = bot.bounds.size.x * (float)(botNum - 1);
            splat_range.z = bot.bounds.size.z * (float)(botNum - 1);

            // splat_range最大，不能超过怪物组包围盒范围的一半
            var collider = GetComponent<Collider>();
            splat_range.x += Mathf.Min(splat_range.x, collider.bounds.extents.x);
            splat_range.z += Mathf.Min(splat_range.z, collider.bounds.extents.z);

            position.x += Random.Range(-splat_range.x, splat_range.x);
            position.z += Random.Range(0f, splat_range.z);//z轴，让怪物集中分布在包围盒的前方
            position.y = 0;

            bots[i].transform.position = position;
            bots[i].transform.parent = transform;

            //bots[i].waveAmplitude = (i + 1) * 0.1f;
            // 45度单位偏移
            //bots[i].waveRadianOffset = (i + 1) * Mathf.PI / 4.0f;

        }
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
