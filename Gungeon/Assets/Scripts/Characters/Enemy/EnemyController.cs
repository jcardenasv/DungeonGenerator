using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType{
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{
    public AudioSource _audioSource;
    public SpriteRenderer spriteRenderer;
    public Sprite darkSprite;
    public Sprite orangeSprite;    
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;
    float _timer = 0;
    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;
    public bool notInRoom = false;
    private Vector3 randomDir;
    public GameObject bulletPrefab;

    public int bossHealth = 20;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState){
            // case(EnemyState.Idle):
            //     Idle();
            // break;
            case(EnemyState.Wander):
                Wander();
            break;
            case(EnemyState.Follow):
                Follow();
            break;
            case(EnemyState.Die):

            break;
            case(EnemyState.Attack):
                _timer -= Time.deltaTime; 
                if(_timer <= 0){
                    Attack();
                }
            break;
        }
        if(!notInRoom){
            if(IsPlayerInRange(range) && currState != EnemyState.Die){
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die){
                currState = EnemyState.Wander;
            }
            if(Vector3.Distance(transform.position, player.transform.position) <=  attackRange){
                currState = EnemyState.Attack;
            }
        } else {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(){
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander(){
        if(!chooseDir){
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }

    void Follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack(){
        if(!coolDownAttack){
            switch(enemyType){
                case(EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    spriteRenderer.sprite = darkSprite;
                    StartCoroutine(CoolDown());
                break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    _timer = 0.8f;
                break;
            }
        }
    }

    private IEnumerator CoolDown(){
        coolDownAttack = true;
        _audioSource.Play();
        yield return new WaitForSeconds(coolDown);
        spriteRenderer.sprite = orangeSprite;
        coolDownAttack = false;
    }

    public void Death(bool boss = false)
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
        if (boss)
        {
            EndGameMenu.EndGame(true);
        }
    }
}
