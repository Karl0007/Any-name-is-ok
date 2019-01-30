using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumUtils;

[CreateAssetMenu(menuName = "ScriptableObjects/Buffs")]
public class BuffData : ScriptableObject
{
	public float BloodData;
	public float PoisonData;
	public float DAFData;
	public float DoubleHarmData;
	public float HpSuckingData;
	public float DefendData;
}

