using UnityEngine;
using UnityEngine.SceneManagement;

public class MinionController : MonoBehaviour
{
    public int ManaCost, Attack, Hp;
    public float MoveSpeed, KnockBack/*, AttackReach*/;

    private AudioManager _audioManager;
    
    // 전역변수 선언
    [HideInInspector] public bool IsEnemy;
    private Vector2 _leftVector, _rightVector;

    private void Start()
    {
        _leftVector = Vector2.left * Time.deltaTime * MoveSpeed;
        _rightVector = Vector2.right * Time.deltaTime * MoveSpeed;

        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        transform.Translate(IsEnemy ? _leftVector : _rightVector);
    }

    public void Init(Minion minion, bool isEnemy, LeftWeapon leftWeapon = null, RightWeapon rightWeapon = null)
    {
        ManaCost = minion.ManaCost;
        Attack = minion.Attack;
        Hp = minion.Hp;
        MoveSpeed = minion.MoveSpeed;
        KnockBack = minion.Knockback;
        
        _leftVector = Vector2.left * Time.deltaTime * MoveSpeed;
        _rightVector = Vector2.right * Time.deltaTime * MoveSpeed;
        
        // _attackReach = minion.AttackReach;
        
        IsEnemy = isEnemy;
        
        if (isEnemy) 
            return;

        var sprite = GetComponentsInChildren<SpriteRenderer>();
        
        sprite[0].sprite = minion.Artwork;

        if (leftWeapon != null)
        {
            sprite[1].sprite = leftWeapon.Artwork;
            
            if (rightWeapon != null) 
                sprite[2].sprite = rightWeapon.Artwork;

            // 왼손 무기 확인
            if (leftWeapon.Id != 0)
            {
                Attack += leftWeapon.AddAttack;
                KnockBack += leftWeapon.AddKnockback;
                ManaCost += leftWeapon.AddManaCost;
            }
        }

        // 오른손 무기 확인
        if (rightWeapon == null || rightWeapon.Id == 0) 
            return;
        
        Hp += rightWeapon.AddHp;
        MoveSpeed += rightWeapon.AddMoveSpeed;
        KnockBack += rightWeapon.AddKnockBack;
        ManaCost += rightWeapon.AddManaCost;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
        case "EnemyMinion":
            if (CompareTag("PlayerMinion"))
                CollisionProcess(other.GetComponent<MinionController>());
            break;
        case "EnemyCastle":
            if (CompareTag("PlayerMinion"))
                SceneManager.LoadScene("Clear");
            break;
        case "PlayerMinion":
            if (CompareTag("EnemyMinion"))
                CollisionProcess(other.GetComponent<MinionController>());
            break;
        case "PlayerCastle":
            if (CompareTag("EnemyMinion"))
                SceneManager.LoadScene("Fail");
            break;
        default:
            return;
        }
        
        _audioManager.Play("Hit");
    }

    public void CollisionProcess(MinionController otherMc)
    {
        Hp -= otherMc.Attack;

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
        
        transform.Translate(IsEnemy ? _rightVector * otherMc.KnockBack * 10 : _leftVector * otherMc.KnockBack * 10);
    }
}
