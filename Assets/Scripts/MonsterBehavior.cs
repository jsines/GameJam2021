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
    public Rigidbody2D rb;
    private float movementSpeed = 1f; 
    private float jumpForce = 500f; 
    
    // State Management 
    public MonsterState currentState = MonsterState.SlamState; 
    [SerializeField] private float stateTimer; 
    [SerializeField] private MonsterState lastState = MonsterState.SlamState; 
    private float timeBetweenStates = 4f; 
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update() { 
        if(currentState == MonsterState.ChargeState){ 
            Charge(); 
        }else if(currentState == MonsterState.SlamState){ 
            Slam(); 
        } 
        FixRotation(); 
        UpdateState(); 
        PlayAnimation();
    } 
    private void UpdateState(){ 
        stateTimer += Time.deltaTime; 
        if(stateTimer >= timeBetweenStates){ 
            stateTimer -= timeBetweenStates;
            if(lastState == MonsterState.FreeState){
                currentState = MonsterState.ChargeState;
            }else if(lastState == MonsterState.ChargeState){
                currentState = MonsterState.SlamState; 
            }else if(lastState == MonsterState.SlamState){ 
                currentState = MonsterState.ChargeState; 
            }
            lastState = currentState;
        }
    }

    private void Charge(){ 
        if(playerPos.localPosition.x > enemyPos.localPosition.x){ 
            rb.AddForce(new Vector2(Mathf.Clamp(Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 120, 0, 350), 0f)); 
        } else if(playerPos.localPosition.x < enemyPos.localPosition.x){ 
            rb.AddForce(new Vector2(-Mathf.Clamp(Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 120, 0, 350), 0f)); 
        } 
        currentState = MonsterState.FreeState;
    } 
    private void Slam(){ 
        if(playerPos.localPosition.x > enemyPos.localPosition.x){
            rb.AddForce(new Vector2(Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 20, jumpForce)); 
        }
        else if(playerPos.localPosition.x < enemyPos.localPosition.x){
            rb.AddForce(new Vector2(-Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 20, jumpForce)); 
        }
        currentState = MonsterState.FreeState;
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
            anim.Play("Monster_Slam");
        }else if(currentState == MonsterState.ChargeState){
            anim.Play("Monster_Charge");
        }else if(rb.velocity.x != 0f){
            anim.Play("Monster_Run");
        }else{
            anim.Play("Monster_Idle");
        }
    }
}