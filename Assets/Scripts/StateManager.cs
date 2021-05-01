using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    GameState gameState;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        gameState = new GameState();
        gameState.turn = 0;          
        button.onClick.AddListener(IncrementState);
    }

    void IncrementState(){
        Debug.Log(gameState.turn);
        gameState.IncrementTurn();
        gameState.SetStateFromTurn();
        Debug.Log(gameState.turn);
        Debug.Log(gameState.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class GameState {
    public int turn;
    public StateEnum currentState;
    public enum StateEnum{
        OpeningDialogue,
        PickConvo,
        PickActivity,
        AtConvo,
        AtActivity,
        EndingDialogue
    }
    public void IncrementTurn(){
        turn++;
    }
    public void SetStateFromTurn(){
        if(turn == 0){
            currentState = StateEnum.OpeningDialogue;
        }else if(turn == 7){
            currentState = StateEnum.EndingDialogue;
        }else if(turn%2 == 0){
            currentState = StateEnum.PickActivity;
        }else{
            currentState = StateEnum.PickConvo;
        }
    } 
    public void RenderState(){
        switch(currentState){
            case StateEnum.OpeningDialogue:
                break;
            case StateEnum.AtConvo:
                break;
        }
    }
}
