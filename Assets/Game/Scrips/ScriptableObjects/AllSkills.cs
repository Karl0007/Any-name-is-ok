﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills")]
public class AllSkills : ScriptableObject
{
	public SkillInfo[] SkillInfos;

}

[System.Serializable]
public class BuffClass
{
	public Buff Type;
	public int Time;
}

[System.Serializable]
public class DebuffClass
{
	public DeBuff Type;
	public int Time;
}

[System.Serializable]
public class SkillInfo {

	public string Name ;
	public string Summary ;
	public float Speed ;
	public float Attack ;
	public float CostMP ;
	public MoveType MoveType;
	public AttackType AttackType ;
	public BuffClass Buff;
	public DebuffClass Debuff ;
	public int DeBuffTime;
	public CostMPType CostMPType;
	public ToolsOnMap ToosOnMap ;
	public Character Character ;
}


