using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	private AudioManager _audioManager;

	private void Awake()
	{
		_audioManager = AudioManager.Instance;
		var sceneName = SceneManager.GetActiveScene().name;
		
		_audioManager.StopNowPlaying();

		switch (sceneName)
		{
			case "Fail":
				_audioManager.Play("Lose");
				break;
			case "Clear":
				_audioManager.Play("Win");
				break;
			case "Start":
				_audioManager.Play("Title");
				break;
			default:
				return;
		}
	}
	
	public void OnStart()
	{
		_audioManager.StopNowPlaying();
		OnButton();
		SceneManager.LoadScene("Tutorial");
	}

	public void OnEnd()
	{
		OnButton();
		Application.Quit();
	}

	public void OnHome()
	{
		_audioManager.Stop("InGame");
		OnButton();
		SceneManager.LoadScene("Start");
	}

	public void OnButton()
	{
		_audioManager.Play("BtnClick");
	}
}
