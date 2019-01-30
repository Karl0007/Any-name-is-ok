using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

[CreateAssetMenu(menuName = "ScriptableObjects/Character")]
public class CharacterDataS : ScriptableObject
{
	public CharacterData[] CharacterInfo;
}

[System.Serializable]
public class CharacterData
{
	public Character Type;
	public float HP;
	public float MP;
	public float Speed;
	public float RE_MP;
	public float Attack;
}