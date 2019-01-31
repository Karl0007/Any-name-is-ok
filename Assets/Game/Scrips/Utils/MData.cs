using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MData : MonoBehaviour
{
	public static MData Instance;
	private void Awake()
	{
		Instance = this;
	}
	public BuffData BuffData;
}