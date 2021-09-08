using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
	public BotGroup botGroupPrefab;

	//������������ɶ��ٹ���
	public float botGroupTotal = 5;
	// ��ǰ�����˶��������
	private int botGroupCount = 0;
	private int score = 0;

	// �������ڵĹ�������
	int spawnBotNum = 1;
	int spawnBotNumMax = 5;

	private int spawnIncreaseCount = 1;
	private int spawnNormalCount = 5;

	bool canSpawnNewGroup = false;

	BotGroup.Type spawnGroupTypeCurrent = BotGroup.Type.Normal;
	BotGroup.Type spawnGroupTypeNext = BotGroup.Type.Normal;

	private float spawnLine;
	private float spawnLineNextOffect = 50.0f;
	public const float spawnLineNextOffectMin = 20.0f; // ������ּ������Сֵ
	public const float spawnLineNextOffectMax = 50.0f; // ������ּ�������ֵ

	private float spawnNextSpeed = 1f;
	public float spawnNextSpeedMin = 1.0f; //�ƶ��ٶȵ���Сֵ
	public float spawnNextSpeedMax = 6.0f; // �ƶ��ٶȵ����ֵ

	private float spawnPosOffect = 15f;

	PlayerCharacter character;

	private void Awake()
	{
		character = FindObjectOfType<PlayerCharacter>();
	}

	void Update()
	{
		GameLoop();
	}

	void GameLoop()
	{
		// ׼������һ��������
		if (!canSpawnNewGroup)
		{
			canSpawnNewGroup = true;

			if (canSpawnNewGroup)
			{
				// ͨ����ҵĵ�ǰλ�ü�����Ҵ������ɹ�����Ĵ���λ��

				if(spawnGroupTypeNext == BotGroup.Type.Normal)
				{
					spawnLine = character.transform.position.z + spawnLineNextOffect;
				}
				else if (spawnGroupTypeNext == BotGroup.Type.Slow)
				{
					spawnLine = character.transform.position.z + 50.0f;
				}
			}
		}

		// d�����û�дﵽ����λ���ǣ��������κι�����
		if(character.transform.position.z <= spawnLine)
		{
			return;
		}

		// ʵ�ʿ�ʼ���ɹ�����
		spawnGroupTypeCurrent = spawnGroupTypeNext;
		SpawnBotGroup(spawnGroupTypeCurrent);

		// �����´γ��ַ���ʱ��������
		spawnBotNum++;
		spawnBotNum = Mathf.Min(spawnBotNum, spawnBotNumMax);

		botGroupCount++;
		canSpawnNewGroup = false;

		// ˢ���²������������
		UpdateNextGroupType();

	}

	private void SpawnBotGroup(BotGroup.Type type)
	{
		float speed = spawnNextSpeed;
		Vector3 spawnPos;
		spawnPos = character.transform.position;
		spawnPos.z += spawnPosOffect;
		switch (spawnGroupTypeCurrent)
		{
			case BotGroup.Type.Slow:
				{
					speed = 1.0f;
				}
				break;

			case BotGroup.Type.Normal:
				{
					speed = spawnNextSpeed;
				}
				break;
		}

		BotGroup botGroup = GameObject.Instantiate<BotGroup>(botGroupPrefab);
		// �������ɵ�Y��λ�ô���������ײ�з�Χ��һ��
		var extents = botGroup.GetComponent<Collider>().bounds.extents;
		spawnPos.y = extents.y / 2;
		botGroup.transform.position = spawnPos;
		// �����������ڲ�����
		botGroup.SpawnBot(spawnBotNum);
		botGroup.runSpeed = speed;
	}

	public void UpdateNextGroupType()
	{
		// ����Ҫ���ɵ���������ʱ����������������
		if(spawnNormalCount > 0)
		{
			float rate;

			// ���ڼ���10�����ڵĹ������
			rate = (float)botGroupCount / 10.0f;
			rate = Mathf.Clamp01(rate);

			//��ɶ�Ĺ���Խ����һ��������ٶ�ҲԽ��
			//ͨ��rate������������С�ٶȼ��ֵ��ʹ����һ�����ٶȡ��뵱ǰ��ɱ��������������
			spawnNextSpeed = Mathf.Lerp(spawnNextSpeedMin, spawnNextSpeedMax, rate);

			spawnNormalCount--;

			if (spawnNormalCount <=0)
			{
				//����ͨ�͹��ﶼ����������һ����ǿ�͹���
				spawnGroupTypeNext = (BotGroup.Type)Random.Range(1, 1);
				spawnIncreaseCount = 1;
			}

			return;
		}

		if (spawnIncreaseCount > 0)
		{
			spawnIncreaseCount--;

			if (spawnIncreaseCount <= 0)
			{
				//
				spawnGroupTypeNext = BotGroup.Type.Normal;
				// ��һ��������ɶ�������ͨ����
				spawnNormalCount = Random.Range(3, 7);
			}

			return;
		}
	}
}
