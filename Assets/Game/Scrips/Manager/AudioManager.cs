using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public static AudioManager Instance;
	private void Awake()
	{
		//Debug.Log("aaaaaaaaaaaaa");
		Instance = this;
	}

	public Dictionary<string,AudioClip> m_AllSounds;
	public AudioSource BGM;
	public AudioSource Effect;
	AudioSource[] m_AllSources;

	// Use this for initialization
	private void Start () {
		//Debug.Log("++++++++++++" + m_AllSounds.Count);
		m_AllSounds = new Dictionary<string, AudioClip>();
		AudioClip[] audioArray = Resources.LoadAll<AudioClip>("Audios");
		foreach (var i in audioArray)
		{
			m_AllSounds.Add(i.name,i);
		}
		m_AllSources = new AudioSource[2];
		m_AllSources = GetComponents<AudioSource>();
		Debug.Log(m_AllSources.Length);
		BGM = m_AllSources[0];
		Effect = m_AllSources[1];
		BGM.loop = true;
		Effect.loop = false;
		BGM.playOnAwake = true;
		Effect.playOnAwake = false;
		PlayBGM("bgm");
		//Debug.Log("++++++++++++"+m_AllSounds.Count);
	}

	public void PlayBGM(string name)
	{
		if (m_AllSounds.ContainsKey(name))
		{
			BGM.clip = m_AllSounds[name];
			BGM.Play();
		}
	}

	public void PlayEffect(string name)
	{
		if (m_AllSounds.ContainsKey(name))
		{
			Effect.clip = m_AllSounds[name];
			Effect.Play();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
