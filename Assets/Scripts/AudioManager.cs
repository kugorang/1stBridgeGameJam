using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	[SerializeField]
	private Sound[] _sounds;

	private string _nowPlay;
	
	// Use this for initialization
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
		foreach (var s in _sounds)
		{
			s.Source = gameObject.AddComponent<AudioSource>();
			s.Source.clip = s.Clip;
			s.Source.volume = s.Volume;
			s.Source.pitch = s.Pitch;
			s.Source.loop = s.Loop;
		}
	}

	private Sound FindSound(string soundName)
	{
		var s = Array.Find(_sounds, sound => sound.Name == soundName);

		if (s != null) 
			return s;
		
		Debug.LogWarningFormat("Sound: {0} not found!", soundName);
		return null;
	}

	public void Play(string soundName)
	{
		var s = FindSound(soundName);
		
		if (s == null)
			return;
		
		s.Source.Play();
		_nowPlay = soundName;
	}
	
	public void Stop(string soundName)
	{
		var s = FindSound(soundName);
		
		if (s == null)
			return;
		
		s.Source.Stop();
		_nowPlay = null;
	}

	public void StopNowPlaying()
	{
		if (_nowPlay == null)
		{
			/*Debug.LogWarningFormat("No music is playing!");*/
			return;
		}
		
		var s = FindSound(_nowPlay);

		s.Source.Stop();
		_nowPlay = null;
	}

	public bool IsPlay(string soundName)
	{
		var s = FindSound(soundName);

		return s != null && s.Source.isPlaying;
	}
}