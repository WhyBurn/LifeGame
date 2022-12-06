using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameControllerObject gco = GameControllerObject.GetGCO();
        if(Input.GetKeyDown(KeyCode.W))
        {
            gco.PlayerDirectionInput(Data.Direction.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gco.PlayerDirectionInput(Data.Direction.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gco.PlayerDirectionInput(Data.Direction.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gco.PlayerDirectionInput(Data.Direction.right);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            gco.PlayerInteractInput();
        }
    }
}
