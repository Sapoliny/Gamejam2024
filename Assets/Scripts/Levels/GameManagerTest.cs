using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum state
{
    Dialogue,
    PlayerAttack,
    BossAttack
}

public class GameManagerTest : MonoBehaviour
{
    state currentState = state.Dialogue;
    state lastState = state.Dialogue;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastState != currentState)
        {
            switch (lastState) //O que acontece ao sair de um estado
            {
                case state.Dialogue:
                    break;
                case state.PlayerAttack:
                    break;
                case state.BossAttack:
                    player.SendMessage("StopDefense");
                    break;
            }

            switch (currentState) //O que acontece ao entrar num estado
            {
                case state.Dialogue:
                    break;
                case state.PlayerAttack:
                    break;
                case state.BossAttack:
                    player.SendMessage("StartDefense");
                    break;
            }
        }

        lastState = currentState;

        switch (currentState) //O que acontece ao estar num estado
        {
            case state.Dialogue:
                break;
            case state.PlayerAttack:
                break;
            case state.BossAttack:
                break;
        }
    }
}
