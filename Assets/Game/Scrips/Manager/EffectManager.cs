using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
	public static EffectManager Instance;
	private void Awake()
	{
		Instance = this;
	}
	public int x;
	public Effects all_Effects;
	public Dictionary<string, GameObject> m_Effects;
	private List<GameObject> SkillEffects;
	private List<GameObject> BuffEffects;

	// Use this for initialization
	void Start () {
		x = 123;
		m_Effects = new Dictionary<string, GameObject>();
		SkillEffects = new List<GameObject>();
		BuffEffects = new List<GameObject>();
		Init();
		//PlaySkillEffects(new Vector3(0, 0, 0), "attack3");
		//PlaySkillEffects(new Vector3(2, 0, 0), "attack3");
		//PlaySkillEffects(new Vector3(4, 0, 0), "attack3");
		Debug.Log("effect ok" + all_Effects.all_Effects[0].name);
	}



	public void Init()
	{

		foreach (var i in all_Effects.all_Effects)
		{
			//Debug.Log(i.name);
			m_Effects.Add(i.name, i);
		}
	}

	public void PlaySkillEffects(Vector3 _position, string name)
	{
		//GameObject tmp = new GameObject();
		//tmp = Instantiate(m_Effects[name]);
		//tmp.transform.position = _position;
		//tmp.GetComponent<ParticleSystem>().Play();
		//Debug.Log(Instantiate(m_Effects[name]));
		SkillEffects.Add(Instantiate(m_Effects[name]));
		SkillEffects[SkillEffects.Count - 1].transform.position = _position;
		SkillEffects[SkillEffects.Count - 1].GetComponent<ParticleSystem>().Play();
	}

	public void PlayBuffEffects(Vector3 _position, string name)
	{
		//GameObject tmp = new GameObject();
		//tmp = Instantiate(m_Effects[name]);
		//tmp.transform.position = _position;
		//tmp.GetComponent<ParticleSystem>().Play();
		//Debug.Log(Instantiate(m_Effects[name]));
		BuffEffects.Add(Instantiate(m_Effects[name]));
		BuffEffects[SkillEffects.Count - 1].transform.position = _position;
		BuffEffects[SkillEffects.Count - 1].GetComponent<ParticleSystem>().Play();
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < SkillEffects.Count; i++)
		{
			if (!SkillEffects[i].GetComponent<ParticleSystem>().isPlaying)
			{
				SkillEffects[i].SetActive(false);
				SkillEffects.RemoveAt(i);
			}
		}
	}

	public void PlayBlood(Vector3 _pos)
	{
		GameObject Blood = GameObject.Find("Blood");
		Blood.transform.position = _pos;
		Blood.GetComponent<ParticleSystem>().Play();
	}

}
