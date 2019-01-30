using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class Skill : MonoBehaviour
{

	private SkillView m_Skillview;
	public SkillInfo m_Skill;
	public AllSkills m_AllSkills;
	public BuffData m_BuffData;

	private Player User { get; set; }
	private int OperatorNum { get; set; }
	private List<int> AttackRange = new List<int>();
	private List<int> MoveRange = new List<int>();
	private int PositionAfterUse { get; set; }
	private int Direction { get; set; }

	// Use this for initialization
	void Start()
	{
		//m_Skill = m_AllSkills.SkillInfos[0];
		//Debug.Log(m_Skill.Name);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private int NearBy(int _x, int _y)
	{
		if (_x < _y)
		{
			return _y - 1;
		}
		if (_x > _y)
		{
			return _y + 1;
		}
		return -1;
	}

	public void IntiSkill(Player _player, int _num)
	{
		User = _player;
		OperatorNum = _num;
		//AttackRange = 
		if (_num == _player.m_Position) Direction = 0;
		if (_num > _player.m_Position) Direction = 1;
		if (_num < _player.m_Position) Direction = -1;
		AttackRange.Clear();
		MoveRange.Clear();
		PreCheck();
		//Debug.Log(OperatorNum);
	}


	private void PreCheck()
	{
		//while (Mathf.Abs(OperatorNum) * m_Skill.CostMP > User.m_MP)
		//{
		//	OperatorNum -= Direction;
		//}
		if (m_Skill.MoveType == MoveType.Flash && OperatorNum == User.Opponent.m_Position)
		{
			OperatorNum -= Direction;
		}
		if (m_Skill.MoveType == MoveType.Roll && OperatorNum == User.Opponent.m_Position)
		{
			OperatorNum -= Direction;
		}
		if (m_Skill.MoveType == MoveType.Move && 
			((Direction == 1 && OperatorNum >= User.Opponent.m_Position && User.Opponent.m_Position > User.m_Position) || 
			(Direction == -1 && OperatorNum <= User.Opponent.m_Position && User.Opponent.m_Position < User.m_Position))) 
		{
			OperatorNum = User.Opponent.m_Position - Direction;
		}

		
	}

	private void MoveWork()//移动动画
	{
		switch (m_Skill.MoveType)
		{
			case MoveType.Roll:
			case MoveType.Move:
				PositionAfterUse = OperatorNum;
				for (int i = User.m_Position; i != OperatorNum; i += Direction)
				{
					MoveRange.Add(i);
				}
				User.m_Position = PositionAfterUse;
				break;
			case MoveType.Flash:
				PositionAfterUse = OperatorNum;
				MoveRange.Add(PositionAfterUse);
				MoveRange.Add(User.m_Position);
				User.m_Position = PositionAfterUse;
				break;
			case MoveType.None:
				PositionAfterUse = User.m_Position;
				break;
		}
		//可以做地图物品判定

	}



	private void BuffWork()//增加BUFF
	{
		BuffClass tmp = new BuffClass();
		tmp = m_Skill.Buff;
		User.m_Buff.Add(tmp); //克隆buff？？
	}

	private void AttackWork()//判定攻击范围
	{
		switch (m_Skill.AttackType) //判定攻击范围
		{
			case AttackType.PointOfChoose:
				AttackRange.Add(OperatorNum);
				break;
			case AttackType.NextBlock:
				AttackRange.Add(PositionAfterUse + Direction);
				break;
			case AttackType.LastBlock:
				AttackRange.Add(PositionAfterUse - Direction);
				break;
			case AttackType.RangeFromPosToChoose:
				for (int i = User.m_Position; i != OperatorNum; i += Direction)
				{
					AttackRange.Add(i);
				}
				break;
			case AttackType.ToMyself:
				break;
			case AttackType.ToOpponent:
				AttackRange.Add(User.Opponent.m_Position);
				break;
			case AttackType.None:
				break;
		}
		if (AttackRange.Count>0)Debug.Log(AttackRange[0]);
	}


	private void DeBuffWork()//伤害生效 增加Debuff
	{
		//if (m_Skill.Attack == 0) return; //非伤害技能
		if (!AttackRange.Exists((int _x) => _x == User.Opponent.m_Position)) return; //技能miss

		float My_Co = 0, Op_Co = -1; //伤害系数

		foreach (var i in User.m_Buff) //己方buff
		{
			switch (i.Type)
			{
				case Buff.CantControlled:
					break;
				case Buff.DefendAndAttack:
					break;
				case Buff.CantDie:
					break;
				case Buff.Hide:
					break;
				case Buff.DoubleHarm:
					Op_Co *= m_BuffData.DoubleHarmData; // 双倍伤害
					break;
				case Buff.HpSucking:
					My_Co = m_BuffData.HpSuckingData; //吸血系数
					break;
				case Buff.None:
					break;
			}
		}

		foreach (var i in User.Opponent.m_Buff)  //对方buff
		{
			switch (i.Type)
			{
				case Buff.CantControlled:
					break;
				case Buff.DefendAndAttack:
					//防守反击成功
					My_Co = Op_Co;
					Op_Co = 0;
					break;
				case Buff.CantDie:
					Op_Co *= m_BuffData.DefendData;
					break;
				case Buff.Hide:
					i.Time = 0;
					break;
				case Buff.DoubleHarm:
					break;
				case Buff.HpSucking:
					break;
				case Buff.None:
					break;
			}
		}
		if (m_Skill.Debuff.Type == DeBuff.Poison)
		{
			Op_Co *= User.Opponent.FindSetBuff(DeBuff.PrePoison);
			My_Co *= User.Opponent.FindSetBuff(DeBuff.PrePoison, 0);// 毒爆 清除debuff
		}
		User.Opponent.m_HP += m_Skill.Attack * Op_Co;
		User.m_HP += m_Skill.Attack * My_Co;

		if (User.Opponent.FindSetBuff(DeBuff.Blood) > 0)
		{
			User.Opponent.m_HP -= m_BuffData.BloodData;//流血系数
		}

		if (User.Opponent.FindSetBuff(Buff.CantDie) > 0 && User.Opponent.m_HP <= 0)
		{
			User.Opponent.m_HP = 1; //不死buff处理
		}

		switch (m_Skill.Debuff.Type)
		{
			case DeBuff.Dizzy:
			case DeBuff.Freeze:
			case DeBuff.Blood:
			case DeBuff.PrePoison:
				DebuffClass tmp = new DebuffClass();
				tmp = m_Skill.Debuff;
				User.Opponent.m_Debuff.Add(tmp); //克隆buff？？
				break;
			case DeBuff.Poison:
				break;
			case DeBuff.NoBuff:
				User.Opponent.m_Buff.Clear();
				break;
			case DeBuff.None:
				break; 
		}

	}

	private void ToolsWork()
	{

	}

	private void CostMPWork()
	{
		if (m_Skill.CostMPType == CostMPType.ByLength)
		{
			User.m_MP -= m_Skill.CostMP * Mathf.Abs(OperatorNum - User.m_Position);
		}
		else
		{
			User.m_MP -= m_Skill.CostMP;
		}
	}

	public void UseSkill()
	{
		//PreCheck();
		CostMPWork();
		MoveWork();
		BuffWork();
		AttackWork();
		DeBuffWork();
		ToolsWork();
	}


	//public void PrepareSkill(Player _player, int _x)
	//{
	//	User = _player;
	//	UseX = _x;
	//	UseX = CheckSkill(User, UseX);
	//	UsedPostion = NextPostion(User, UseX);
	//}



	//public int NextPostion(Player _player,int _x)
	//{
	//	if (Move || Flash)
	//	{
	//		return _player.Position + _x;
	//	}
	//	else
	//	{
	//		return _player.Position;
	//	}
	//}
	//public 

}
