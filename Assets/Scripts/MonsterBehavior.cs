using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public enum MonsterState{ 
    FreeState, 
    ChargeState, 
    SlamState 
} 
public class MonsterBehavior : MonoBehaviour {  
    public Transform playerPos; 
    public Transform enemyPos; 
    private bool facingRight;  
    private Animator anim;
    private AudioSource aSrc;
    public Rigidbody2D rb;
    [SerializeField]private float movementSpeed = 500f; 
    private float jumpForce = 800f;
    public int waitTime = 180;
    private int waitTimeTimer = 0;

    private bool actioned = false;
    
    // State Management 
    public MonsterState currentState = MonsterState.SlamState; 
    [SerializeField] private float stateTimer; 
    [SerializeField] private MonsterState lastState = MonsterState.SlamState; 
    private float timeBetweenStates = 4f; 

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aSrc = GetComponent<AudioSource>();
    }
    
    void Update() { 
        if(waitTimeTimer >= waitTime){
            if(currentState == MonsterState.ChargeState){ 
                Charge(); 
            }else if(currentState == MonsterState.SlamState){ 
                Slam(); 
            } 
            FixRotation(); 
            UpdateState(); 
            PlayAnimation();
        }
        if(currentState == MonsterState.SlamState && rb.velocity.y == 0){
            aSrc.Play();
        }
        waitTimeTimer += 1;
    } 

    private void UpdateState(){ 
        stateTimer += Time.deltaTime; 
        if(stateTimer >= timeBetweenStates){ 
            stateTimer -= timeBetweenStates;
            if(lastState == MonsterState.ChargeState){
                currentState = MonsterState.SlamState; 
                actioned = false;
            }else if(lastState == MonsterState.SlamState){ 
                currentState = MonsterState.ChargeState; 
                actioned = false;
            }
            lastState = currentState;
        }
    }

    private void Charge(){ 
        if(actioned == false){
            float diff = playerPos.localPosition.x - enemyPos.localPosition.x;
            if(diff > 0){ 
                rb.AddForce(new Vector2(2000f, 0f)); 
            } else if(diff < 0){ 
                rb.AddForce(new Vector2(-2000f, 0f)); 
            } 
        }
        actioned = true;
    } 
    private void Slam(){ 
        if(actioned == false){
            if(playerPos.localPosition.x > enemyPos.localPosition.x){
                rb.AddForce(new Vector2(Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 20, jumpForce)); 
            }
            else if(playerPos.localPosition.x < enemyPos.localPosition.x){
                rb.AddForce(new Vector2(-Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 20, jumpForce)); 
            }
        } 
        actioned = true;
    }
    private void FixRotation(){ 
        if(rb.velocity.x < 0 && !facingRight){ 
            FlipCharacter(); 
        }else if(rb.velocity.x > 0 && facingRight){ 
            FlipCharacter();
        } 
    } 
    private void FlipCharacter(){ 
        facingRight = !facingRight; 
        transform.Rotate(0f, 180f, 0f); 
    } 
    private void PlayAnimation(){
        if(rb.velocity.y != 0f){
            anim.Play("Monster_Slam2");
        }else if(Mathf.Abs(rb.velocity.x) < 0.2){
            anim.Play("Monster_Idle2");
        }else if(currentState == MonsterState.ChargeState){
            anim.Play("Monster_Charge2");
        }else if(rb.velocity.x != 0f){
            anim.Play("Monster_Run2");
        }else{
            anim.Play("Monster_Idle2");
        }
    }
}