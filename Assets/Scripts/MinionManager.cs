using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MinionManager : MonoBehaviour
{
	private StageManager _stageManager;
	
	// 전역 변수
	[SerializeField] private int _randMax;
	private int _leftWeaponId, _rightWeaponId;

	public Image LeftWeaponIcon, RightWeaponIcon;
	public Image LeftWeaponResult, RightWeaponResult;

	[SerializeField] private TextMeshProUGUI _textMeshProUgui;
	
	// Minion Prefab
	[SerializeField] private GameObject _minionPrefab;
	[SerializeField] private GameObject[] _enemyPrefab;
	
	// Minion Spawn Zone
	[SerializeField] private Transform _playerZone, _enemyZone;
	
	// Scriptable Object
	[SerializeField] private Minion[] _minions;
	[SerializeField] private Minion[] _enemy;
	[SerializeField] private LeftWeapon[] _leftWeapons;
	[SerializeField] private RightWeapon[] _rightWeapons;

	[SerializeField] private Sprite[] _leftIcon;
	[SerializeField] private Sprite[] _rightIcon;
	
	private void Start()
	{
		_stageManager = GetComponent<StageManager>();
	}

	public void OnSendOut()
	{
		// TODO: 이 코드는 _minions 배열이 등장 확률을 기준으로 오름차순임을 가정하므로 실행 전 Minion 배열 반드시 확인
		// ex) hero - 10, upgrade - 30, nomral - 60 이런 식으로
		var randVal = Random.Range(0, _randMax);
		
		foreach (var minion in _minions)
		{
			if (randVal > minion.AppearPercent)
				continue;

			if (!_stageManager.ManaDown(minion.ManaCost + _leftWeapons[_leftWeaponId].AddManaCost +
			                           _rightWeapons[_rightWeaponId].AddManaCost))
				break;

			// TODO: 시간 남으면 ObjectPool 코드로 리팩토링 할 것. 매우 비효율적인 코드임.
			var obj = Instantiate(_minionPrefab);

			obj.transform.position = _playerZone.transform.position;
			
			var minionController = obj.GetComponent<MinionController>();
			
			minionController.Init(minion, false, _leftWeapons[_leftWeaponId], _rightWeapons[_rightWeaponId]);
			
			break;
		}

		_textMeshProUgui.text = string.Format("Mana : {0}", 
			20 + _leftWeapons[_leftWeaponId].AddManaCost + _rightWeapons[_rightWeaponId].AddManaCost);
		
		/*LeftWeaponImg.sprite = LeftWeaponDefault;
		RightWeaponImg.sprite = RightWeaponDefault;*/
	}

	public void OnSetLeftWeapon(int leftWeaponId)
	{
		_leftWeaponId = leftWeaponId;
		LeftWeaponIcon.sprite = _leftIcon[leftWeaponId];
		LeftWeaponResult.sprite = _leftWeapons[leftWeaponId].Artwork;

		_textMeshProUgui.text = string.Format("Mana : {0}", 
			20 + _leftWeapons[leftWeaponId].AddManaCost + _rightWeapons[_rightWeaponId].AddManaCost);
	}

	public void OnSetRightWeapon(int rightWeaponId)
	{
		_rightWeaponId = rightWeaponId;
		RightWeaponIcon.sprite = _rightIcon[rightWeaponId];
		RightWeaponResult.sprite = _rightWeapons[rightWeaponId].Artwork;
		
		_textMeshProUgui.text = string.Format("Mana : {0}", 
			20 + _leftWeapons[_leftWeaponId].AddManaCost + _rightWeapons[rightWeaponId].AddManaCost);
	}

	public void EnemySpawn(int enemyId)
	{
		// TODO: 시간 남으면 ObjectPool 코드로 리팩토링 할 것. 매우 비효율적인 코드임.
		var obj = Instantiate(_enemyPrefab[enemyId]);

		obj.transform.position = _enemyZone.transform.position;
			
		var minionController = obj.GetComponent<MinionController>();
			
		minionController.Init(_enemy[enemyId], true);
	}
}
