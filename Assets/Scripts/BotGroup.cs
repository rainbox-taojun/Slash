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
        Vector3 blowout;        // �����ɢ�ķ���
        Vector3 blowoutUp;      // ��ɢ����Ĵ�ֱ����
        Vector3 blowoutRight;   // ��ɢ�����ˮƽ����

        float topRadius;        // Բ׶�Ķ���뾶
        float arcLength;        // Բ׶�����ɢ��Χ����������bot��������

        float upAngle;          // ��Y����ת�Ƕ�
        float upAngleCenter;    // ��ʼ����
        float upAngleSwing;     // ƫ�ƽ�
        float rightAngle;       // ��X����ת�Ƕ�

        float blowoutSpeed;     // �ٶ�
        float blowoutSpeedVary; // �ٶȱ仯����

        // ------------------------------
        topRadius = 0.3f;
        blowoutSpeedVary = 10.0f;
        rightAngle = 40.0f;
        upAngleCenter = 180.0f;
        upAngleSwing = 10.0f;
        // ------------------------------

        // ���һ��Բ׶ǰ����б�ĽǶ�
        rightAngle += Random.Range(-5.0f, 5.0f);

        // �������ڹ�����������Բ�ܶ���ķ�ɢ���ȣ����ܳ���120
        arcLength = bots.Length * 30.0f;
        arcLength = Mathf.Min(arcLength, 120.0f);

        // Χ��Y�����ת��
        upAngle = upAngleCenter;
        upAngle += upAngleSwing;

        // ����ÿһ������������ǵķ�ɢ���
        foreach(BotCharacter bot in bots)
		{
            // ��ɢ����
            blowoutUp = Vector3.up; // ��ɢԲ׶��������
            blowoutRight = Vector3.forward * topRadius; // ��ɢԲ׶�Ķ������ĵ������Ե������
            blowoutRight = Quaternion.AngleAxis(upAngle, Vector3.up) * blowoutRight; // ����һ���õ���������Y����תupAngle
            blowout = blowoutUp + blowoutRight; // ��������ӵõ������ɢ����ĵ�������
            blowout.Normalize();

            // ��ɢ���������ǰ����б
            blowout = Quaternion.AngleAxis(rightAngle, Vector3.right) * blowout;

            // ��ɢ�����ٶ�
            blowoutSpeed = blowoutSpeedVary * Random.Range(0.8f, 1.5f);
            blowout *= blowoutSpeed;

            // ���ٶ�
            // ��˺�õ��ĵ������������ɢ�����Y�ᴹֱ��������ת����ķ���
            Vector3 angular_velocity = Vector3.Cross(Vector3.up, blowout);
            angular_velocity.Normalize();
            angular_velocity *= Random.Range(0.5f, 1.5f) * blowoutSpeed;

            // Ӧ��
            bot.Blowout(blowout, angular_velocity);

            // �ı�Y����ת�ǣ���ÿ������ķ�ɢ���򣬶��ڶ����ɢ�����Ĳ�ͬλ��
            upAngle += arcLength / (bots.Length);
        }

        Destroy(this.gameObject);
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
