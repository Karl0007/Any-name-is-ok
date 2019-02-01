using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class Skill : MonoBehaviour
{

	private SkillView m_Skillview;
	public SkillInfo m_Skill;
	public AllSkills m_AllSkills;
	public  BuffData m_BuffData;

	private Player User { get; set; }
	public int OperatorNum { get; set; }
	public List<int> AttackRange = new List<int>();
	public List<int> MoveRange = new List<int>();
	private int PositionAfterUse { get; set; }
	private int Direction { get; set; }

	public Skill Clone()
	{
		Skill tmp = new Skill
		{
			m_AllSkills = m_AllSkills,
			m_BuffData = m_BuffData,
			m_Skill = m_Skill,
			AttackRange = AttackRange,
			MoveRange = MoveRange
		};
		return tmp;
	}

	// Use this for initialization
	void Start()
	{
		//Debug.Log(m_AllSkills.SkillInfos[0].Name);
		//Debug.Log("++++"+m_BuffData.DAFData);
		//Debug.Log(m_BuffData.BloodData);
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

	public bool IntiSkill(Player _player, int _num)
	{
		User = _player;
		OperatorNum = _num;
		//AttackRange = 
		//if (_num == _player.m_Position) Direction = 0;
		if (_num > _player.m_Position) Direction = 1;
		if (_num < _player.m_Position) Direction = -1;
		AttackRange.Clear();
		MoveRange.Clear();
		return PreCheck();
		//Debug.Log(OperatorNum);
	}

	public void GetRange(Player _player,int _num)
	{
		if (!IntiSkill(_player, _num))
		{
			if (GameManager.Instance.m_GameState == GameState.GetP1InputOper)
				GameManager.Instance.m_GameState = GameState.GetP1InputSkill;
			if (GameManager.Instance.m_GameState == GameState.GetP2InputOper)
				GameManager.Instance.m_GameState = GameState.GetP2InputSkill;

		}
		else
		{
			GetMoveRange();
			GetAttackRange();
		}

	}

	private bool PreCheck()
	{
		if (m_Skill.CostMPType == CostMPType.OneTime && User.m_MP < m_Skill.CostMP) return false;
		if (m_Skill.CostMPType == CostMPType.ByLength)
		{
			bool tmp = true;
			while (Mathf.Abs(OperatorNum - User.m_Position) * m_Skill.CostMP > User.m_MP && OperatorNum != User.m_Position)
			{
				tmp = false;
				OperatorNum -= Direction;
			}
			if (OperatorNum == User.m_Position) return tmp || m_Skill.Name == "移动";
		}
		//if (m_Skill.AttackType == AttackType.LastBlock)
		//{
			//OperatorNum += Direction;
		//}
		//if (m_Skill.AttackType == AttackType.NextBlock)
		//{
		//	OperatorNum -= Direction;
		//}
		//if (m_Skill.cos)
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
		return true;
		
	}

	private void GetMoveRange()
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
				MoveRange.Add(OperatorNum);
				//User.m_Position = PositionAfterUse;
				break;
			case MoveType.Flash:
				PositionAfterUse = OperatorNum;
				MoveRange.Add(PositionAfterUse);
				MoveRange.Add(User.m_Position);
				//User.m_Position = PositionAfterUse;
				break;
			case MoveType.None:
				PositionAfterUse = User.m_Position;
				MoveRange.Add(User.m_Position);
				break;
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
				MoveRange.Add(OperatorNum);
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
				MoveRange.Add(User.m_Position);
				break;
		}
		//可以做地图物品判定

	}



	private void BuffWork()//增加BUFF
	{
		//BuffClass tmp = new BuffClass();
		//tmp = m_Skill.Buff;
		if (m_Skill.Buff.Type != Buff.None)User.m_Buff.Add(m_Skill.Buff.Clone()); //克隆buff？？
		switch (m_Skill.Buff.Type)
		{
			case Buff.CantDie:
				User.cantdie.GetComponent<ParticleSystem>().Play();
				break;
			case Buff.DefendAndAttack:
				User.defend.GetComponent<ParticleSystem>().Play();
				break;
			case Buff.DoubleHarm:
				User.doubleattack.GetComponent<ParticleSystem>().Play();
				break;
		}
	}

	private void GetAttackRange()
	{
		switch (m_Skill.AttackType) //判定攻击范围
		{
			case AttackType.PointOfChoose:
				AttackRange.Add(OperatorNum);
				break;
			case AttackType.NextBlock:
				if (InRange(PositionAfterUse + Direction)) AttackRange.Add(PositionAfterUse + Direction);
				break;
			case AttackType.LastBlock:
				if (InRange(PositionAfterUse - Direction)) AttackRange.Add(PositionAfterUse - Direction);
				break;
			case AttackType.RangeFromPosToChoose:
				for (int i = User.m_Position; i != OperatorNum; i += Direction)
				{
					AttackRange.Add(i);
				}
				AttackRange.Add(OperatorNum);
				break;
			case AttackType.ToMyself:
				break;
			case AttackType.ToOpponent:
				AttackRange.Add(User.Opponent.m_Position);
				break;
			case AttackType.None:
				break;
		}
	}

	private void AttackWork()//判定攻击范围
	{
		GetAttackRange();
		if (!AttackRange.Exists((int _x) => _x == User.Opponent.m_Position))
		{
			if(m_Skill.AttackType!= AttackType.None)UIManager.Instance.AddMessage("MISS！");
			AudioManager.Instance.PlayEffect("瞬移");
			return; //技能miss
		}
		else
		{
			switch (User.m_CharacterType)
			{
				case Character.None:
					break;
				case Character.Warrior:
					AudioManager.Instance.PlayEffect("流血攻击");
					break;
				case Character.Mage:
					AudioManager.Instance.PlayEffect("法术");
					break;
				case Character.Assassin:
					AudioManager.Instance.PlayEffect("刺客");
					break;
			}

		}
		UIManager.Instance.AddMessage("技能命中了！");
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
					UIManager.Instance.AddMessage("P"+(User.m_ID+1)+"打出了双倍伤害！");
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
					UIManager.Instance.AddMessage("P" + (User.Opponent.m_ID+1) + "防守反击成功！");
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

		switch (m_Skill.Debuff.Type)
		{
			case DeBuff.Dizzy:
			case DeBuff.Freeze:
			case DeBuff.Blood:
			case DeBuff.PrePoison:
				//DebuffClass tmp = new DebuffClass();
				//tmp = m_Skill.Debuff;
				User.Opponent.m_Debuff.Add(m_Skill.Debuff.Clone()); //克隆buff？？
				break;
			case DeBuff.Poison:
				break;
			case DeBuff.NoBuff:
				User.Opponent.m_Buff.Clear();
				break;
			case DeBuff.None:
				break;
		}
		switch (m_Skill.Debuff.Type)
		{
			case DeBuff.Dizzy:
				User.Opponent.dizzy.GetComponent<ParticleSystem>().Play();
				break;
			case DeBuff.Blood:
				User.Opponent.blood.GetComponent<ParticleSystem>().Play();
				break;
			case DeBuff.PrePoison:
				User.Opponent.poison.GetComponent<ParticleSystem>().Play();
				break;
		}
		//if (AttackRange.Count>0)Debug.Log(AttackRange[0]);
	}


	private void DeBuffWork()//伤害生效 增加Debuff
	{
		//if (m_Skill.Attack == 0) return; //非伤害技能

		//Debug.Log(m_BuffData.BloodData);
		if (User.Opponent.FindSetBuff(DeBuff.Blood) > 0)
		{
			User.Opponent.m_HP -= m_BuffData.BloodData;//流血系数
			UIManager.Instance.AddMessage("P" + (User.Opponent.m_ID + 1) + "正在流血 持续受到伤害！");
			Debug.Log("流血造成伤害");
		}
		if (User.Opponent.FindSetBuff(Buff.CantDie) > 0 && User.Opponent.m_HP <= 0)
		{
			User.Opponent.m_HP = 1; //不死buff处理
			UIManager.Instance.AddMessage("P" + (User.Opponent.m_ID + 1) + "不死之身触发了！");
			Debug.Log("坚毅不倒");
		}
		//Debug.Log(User.Opponent.FindSetBuff(DeBuff.Blood));

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
			Debug.Log(m_Skill.CostMP);
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

	private bool InRange(int x)
	{
		return (x >= 1 && x <= 14);
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
