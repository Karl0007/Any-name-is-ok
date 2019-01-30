using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumUtils
{
	public enum Character
	{
		Warrior,
		Mage,
		Assassin,
		None
	}

	public enum Buff
	{
		CantControlled,
		DefendAndAttack,
		CantDie,
		Hide,
		DoubleHarm,
		HpSucking,
		None
	}

	public enum DeBuff
	{
		Dizzy,
		Blood,
		Freeze,
		PrePoison,
		NoBuff,
		None
	}

	public enum MoveType
	{
		Move,
		Flash,
		Roll,
		None
	}

	public enum AttackType
	{
		PointOfChoose,
		NextBlock,
		LastBlock,
		RangeFromPosToChoose,
		ToMyself,
		ToOpponent,
		None
	}

	public enum ToolsOnMap
	{
		AddHP,
		AddMP,
		Trap,
		None
	}
}
