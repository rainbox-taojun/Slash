using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGroup : MonoBehaviour
{
    BotCharacter[] bots;

    public BotCharacter botPrefab;

    public float runSpeed;

    //�ɽ�
    public void GroupBlowout()
    {

    }

    // ���ɹ���
    public void SpawnBot(int botNum)
    {
        bots = new BotCharacter[botNum];
        Vector3 position;
        for (int i = 0; i<botNum;i++)
		{
            position = transform.position;
            BotCharacter bot = Instantiate<BotCharacter>(botPrefab);
            bots[i] = bot;

            //���ݹ�����������ɢ��Χ�����޶���groupBox��

            Vector3 splat_range;
            // ���ݹ������������������X��Z���ϵķֲ���Χ
            splat_range.x = bot.bounds.size.x * (float)(botNum - 1);
            splat_range.z = bot.bounds.size.z * (float)(botNum - 1);

            // splat_range��󣬲��ܳ����������Χ�з�Χ��һ��
            var collider = GetComponent<Collider>();
            splat_range.x += Mathf.Min(splat_range.x, collider.bounds.extents.x);
            splat_range.z += Mathf.Min(splat_range.z, collider.bounds.extents.z);

            position.x += Random.Range(-splat_range.x, splat_range.x);
            position.z += Random.Range(0f, splat_range.z);//z�ᣬ�ù��Ｏ�зֲ��ڰ�Χ�е�ǰ��
            position.y = 0;

            bots[i].transform.position = position;
            bots[i].transform.parent = transform;

            //bots[i].waveAmplitude = (i + 1) * 0.1f;
            // 45�ȵ�λƫ��
            //bots[i].waveRadianOffset = (i + 1) * Mathf.PI / 4.0f;

        }
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
