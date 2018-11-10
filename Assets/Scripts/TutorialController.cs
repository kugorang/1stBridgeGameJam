using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
	public GameObject[] Text;
	private int _nowIndex, _maxIndex;

	private AudioManager _audioManager;

	private void Start()
	{
		_nowIndex = 1;
		_maxIndex = Text.Length;
		_audioManager = AudioManager.Instance;

		_audioManager.Stop("Title");
		_audioManager.Play("InGame");
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0)) 
			return;

		if (_nowIndex >= _maxIndex)
		{
			_audioManager.StopNowPlaying();
			SceneManager.LoadScene("Stage01");
		}
		else
			Text[_nowIndex++].SetActive(true);
		
		_audioManager.Play("BtnClick");
	}
}
