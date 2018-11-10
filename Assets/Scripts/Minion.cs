using UnityEngine;

public enum MinionType
{
	Normal,
	Upgrade,
	Hero
};

[CreateAssetMenu(fileName = "New Minon", menuName = "Create Minion")]
public class Minion : ScriptableObject
{
	// 병사 타입
	public MinionType Type;
	
	// 병사 이미지
	public Sprite Artwork;
	
	// 병사 마나 비용
	public int ManaCost;
	
	// 병사 확률
	public int AppearPercent;
	
	// 공격 관련
	public int Attack;
	public float Knockback;
	//public float AttackReach;
	
	// 방어 관련
	public int Hp;
	public float MoveSpeed;

	// 무기 관련
	public int LwId;
	public int RwId;
}
