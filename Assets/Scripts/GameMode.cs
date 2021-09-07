using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
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
					spawnLine = Character.transform.position.z + spawnLineNextOffect;
				}
				else if (spawngroupTypeNext == BotGroup.Type.Slow)
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
}
