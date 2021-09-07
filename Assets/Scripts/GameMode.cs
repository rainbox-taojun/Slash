using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
	//本怪物最多生成多少怪物
	public float botGroupTotal = 5;
	// 当前生成了多少组怪物
	private int botGroupCount = 0;
	private int score = 0;

	// 怪物组内的怪物数量
	int spawnBotNum = 1;
	int spawnBotNumMax = 5;

	private int spawnIncreaseCount = 1;
	private int spawnNormalCount = 5;

	bool canSpawnNewGroup = false;

	BotGroup.Type spawnGroupTypeCurrent = BotGroup.Type.Normal;
	BotGroup.Type spawnGroupTypeNext = BotGroup.Type.Normal;

	private float spawnLine;
	private float spawnLineNextOffect = 50.0f;
	public const float spawnLineNextOffectMin = 20.0f; // 怪物出现间隔的最小值
	public const float spawnLineNextOffectMax = 50.0f; // 怪物出现间隔的最大值

	private float spawnNextSpeed = 1f;
	public float spawnNextSpeedMin = 1.0f; //移动速度的最小值
	public float spawnNextSpeedMax = 6.0f; // 移动速度的最大值

	private float spawnPosOffect = 15f;

	PlayerCharacter character;

	void GameLoop()
	{
		// 准备生成一波怪物组
		if (!canSpawnNewGroup)
		{
			canSpawnNewGroup = true;

			if (canSpawnNewGroup)
			{
				// 通过玩家的当前位置计算玩家触发生成怪物组的触发位置

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

		// d当玩家没有达到触发位置是，不生成任何怪物组
		if(character.transform.position.z <= spawnLine)
		{
			return;
		}

		// 实际开始生成怪物组
		spawnGroupTypeCurrent = spawnGroupTypeNext;
		SpawnBotGroup(spawnGroupTypeCurrent);

		// 更新下次出现分组时怪物数量
		spawnBotNum++;
		spawnBotNum = Mathf.Min(spawnBotNum, spawnBotNumMax);

		botGroupCount++;
		canSpawnNewGroup = false;

		// 刷新下拨怪物组的类型
		UpdateNextGroupType();

	}
}
