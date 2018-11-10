using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour 
{
	// Use this for initialization
	private void Start () 
	{
		SceneManager.LoadScene("Start");
	}
}
