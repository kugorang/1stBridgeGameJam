using UnityEngine;

public enum RightWeaponType
{
	None,
	Chip,
	Bread,
	Choco
};

[CreateAssetMenu(fileName = "New RightWeapon", menuName = "Create RightWeapon")]
public class RightWeapon : ScriptableObject 
{
	// 무기 아이디
	public int Id;
	public RightWeaponType Type;
	
	// 무기 이미지
	public Sprite Artwork;
	
	// 무기 설명
	public string Name;
	public string Description;
	
	// 인게임 적용 수치
	public int AddHp;
	public float AddMoveSpeed;
	public float AddKnockBack;
	public int AddManaCost;
}
