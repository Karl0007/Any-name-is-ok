using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumUtils
{
	public enum Character
	{
		None,
		Warrior,
		Mage,
		Assassin,
	}

	public enum Buff
	{
		None,
		CantControlled,
		DefendAndAttack,
		CantDie,
		Hide,
		DoubleHarm,
		HpSucking,
	}

	public enum DeBuff
	{
		None,
		Dizzy,
		Blood,
		Freeze,
		PrePoison,
		Poison,
		NoBuff
	}

	public enum MoveType
	{
		None,
		Move,
		Flash,
		Roll,
	}

	public enum AttackType
	{
		None,
		PointOfChoose,
		NextBlock,
		LastBlock,
		RangeFromPosToChoose,
		ToMyself,
		ToOpponent
	}

	public enum ToolsOnMap
	{
		None,
		AddHP,
		AddMP,
		Trap
	}

	public enum CostMPType
	{
		OneTime,
		ByLength
	}

	public enum GameState
	{
		GetP1InputSkill,
		GetP1InputOper,
		GetP2InputSkill,
		GetP2InputOper,
		WorkInput,
		GetResult
	}
}
