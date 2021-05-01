using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public enum MonsterState{ 
    FreeState, 
    ChargeState, 
    SlamState 
} 
public class MonsterBehavior : MonoBehaviour { 
    public Rigidbody2D rb; 
    public Transform playerPos; 
    public Transform enemyPos; 
    public float movementSpeed; 
    private bool facingRight; 
    public float jumpForce; 
    
    // State Management 
    [SerializeField] private MonsterState currentState = MonsterState.SlamState; 
    [SerializeField] private float stateTimer; 
    [SerializeField] private MonsterState lastState = MonsterState.SlamState; 
    private float timeBetweenStates = 4f; 
    void Update() { 
        if(currentState == MonsterState.ChargeState){ 
            Charge(); 
        }else if(currentState == MonsterState.SlamState){ 
            Slam(); 
        } 
        FixRotation(); 
        UpdateState(); 
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
            rb.AddForce(new Vector2(Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 85, 0f)); 
        } else if(playerPos.localPosition.x < enemyPos.localPosition.x){ 
            rb.AddForce(new Vector2(-Mathf.Abs(playerPos.localPosition.x - enemyPos.localPosition.x) * 85, 0f)); 
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
}