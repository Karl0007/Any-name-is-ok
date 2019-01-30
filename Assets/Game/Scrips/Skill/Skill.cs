using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

public class Skill : MonoBehaviour {

	private SkillView m_Skillview;
	private SkillInfo m_Skill;
	public AllSkills m_AllSkills;

	private Player User { get; set; }
	private int OperatorNum {get; set;}
	private List<int> AttackRange { get; set; }
	private List<int> MoveRange { get; set; }
	private int PositionAfterUse { get; set; }
	private int Direction { get; set; }
		
	// Use this for initialization
	void Start () {
		m_Skill = m_AllSkills.SkillInfos[0];
		Debug.Log(m_Skill.Name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private int NearBy(int _x,int _y)
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

	public void IntiSkill(Player _player,int _num)
	{
		User = _player;
		OperatorNum = _num;
		if (_num == _player.m_Position) Direction = 0;
		if (_num > _player.m_Position) Direction = 1;
		if (_num < _player.m_Position) Direction = -1;
		AttackRange.Clear();
		MoveRange.Clear();
		
	}

	private void PreCheck()
	{
		
	}

	private void MoveWork()
	{
		switch (m_Skill.MoveType)
		{
			case MoveType.Roll:
			case MoveType.Move:
				PositionAfterUse = OperatorNum;
				for (int i=User.m_Position; i!=OperatorNum; i += Direction)
				{
					MoveRange.Add(i);
				}
				break;
			case MoveType.Flash:
				PositionAfterUse = OperatorNum;
				MoveRange.Add(PositionAfterUse);
				MoveRange.Add(User.m_Position);
				break;
			case MoveType.None:
				break;
		}
	}



	private void BuffWork()
	{
		User.m_Buff.Add(m_Skill.Buff);
	}

	private void AttackWork()
	{
		switch (m_Skill.AttackType)
		{
			case AttackType.PointOfChoose:
				AttackRange.Add(OperatorNum);
				break;
			case AttackType.NextBlock:
				AttackRange.Add(PositionAfterUse + 1);
				break;
			case AttackType.LastBlock:
				AttackRange.Add(PositionAfterUse - 1);
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
		if (AttackRange.Find((User =>) {pos == User.Opponent.m_Position}))
		{

		}
	}

	private void DeBuffWork()
	{

	}

	private void ToolsWork()
	{

	}

	public void UseSkill()
	{
		PreCheck();
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

	//public int CheckSkill(Player _player,int _x)
	//{
	//	while (Mathf.Abs(_x) * CostMP > _player.MP)
	//	{
	//		if (_x > 0) _x--; else _x++;
	//	}
	//	if (Flash && _player.Position+_x == _player.Opponent.Position)
	//	{
	//		return NearBy(_player.Position, _player.Opponent.Position) - _player.Position;
	//	}
	//	if (Move && NearBy(_player.Position+_x,_player.Opponent.Position) != NearBy(_player.Position,_player.Opponent.Position))
	//	{
	//		return NearBy(_player.Position,_player.Opponent.Position) - _player.Position;
	//	}
	//	return _x;
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
