using UnityEngine;

public enum LeftWeaponType
{
	None,
	Sword,
	Spear,
	Ball
};

[CreateAssetMenu(fileName = "New LeftWeapon", menuName = "Create LeftWeapon")]
public class LeftWeapon : ScriptableObject
{
	// 무기 아이디
	public int Id;
	public LeftWeaponType Type;
	
	// 무기 이미지
	public Sprite Artwork;
	
	// 무기 설명
	public string Name;
	public string Description;
	
	// 인게임 적용 수치 (추가)
	public int AddAttack;
	public float AddKnockback;
	//public float AddAttackReach;
	public int AddManaCost;
}