using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
	[SerializeField] private Transform _mainCamera;
	[SerializeField] private float _cameraMoveSpeed;
	[SerializeField] private float _cameraLeftEnd;
	[SerializeField] private float _cameraRightEnd;
	
	// UI
	[SerializeField] private TextMeshProUGUI _manaText;
	
	// 전역변수 선언
	private bool _isLeft, _isPressed;
	private Vector3 _leftVector, _rightVector;

	private int _mana;
	[SerializeField] private float _manaTimeGap;
	[SerializeField] private float _respawnTimeGap;
	[SerializeField] private float _enemy1SpawnTimeGap;
	[SerializeField] private float _enemy2SpawnTimeGap;

	private MinionManager _minionManager;

	private void Start()
	{
		_leftVector = Vector2.left * Time.deltaTime * _cameraMoveSpeed;
		_rightVector = Vector2.right * Time.deltaTime * _cameraMoveSpeed;

		_minionManager = GetComponent<MinionManager>();

		Invoke("ManaUp", _manaTimeGap);
		StartCoroutine("EnemySpawn");
	}

	private void Update()
	{
		if (!_isPressed)
			return;

		if (_isLeft)
		{
			if (_mainCamera.position.x > _cameraLeftEnd)
				_mainCamera.transform.Translate(_leftVector);
		}
		else
		{
			if (_mainCamera.position.x < _cameraRightEnd)
				_mainCamera.transform.Translate(_rightVector);
		}
	}

	private void ManaUp()
	{
		Invoke("ManaUp", _manaTimeGap);
		
		if (_mana >= 300)
			return;
		
		_mana += 1;
		_manaText.text = _mana.ToString();
	}

	public bool ManaDown(int mana)
	{
		if (mana > _mana)
			return false;

		_mana -= mana;
		_manaText.text = _mana.ToString();

		return true;
	}

	private IEnumerator EnemySpawn()
	{
		yield return new WaitForSeconds(4.0f);
		
		while (true)
		{
			StartCoroutine("Enemy1Spawn");
			StartCoroutine("Enemy2Spawn");
			
			yield return new WaitForSeconds(_respawnTimeGap);
		}
	}

	private IEnumerator Enemy1Spawn()
	{
		for (var i = 0; i < 5; i++)
		{
			// 적 생성 코드
			_minionManager.EnemySpawn(0);
			
			yield return new WaitForSeconds(_enemy1SpawnTimeGap);
		}

		yield return null;
	}

	private IEnumerator Enemy2Spawn()
	{
		for (var i = 0; i < 1; i++)
		{
			// 적 생성 코드
			_minionManager.EnemySpawn(1);
			
			yield return new WaitForSeconds(_enemy2SpawnTimeGap);
		}
	}

	public void OnCameraMove(bool isLeft)
	{
		_isLeft = isLeft;
		_isPressed = true;
	}

	public void OnPressedUp()
	{
		_isPressed = false;
	}
	
}
