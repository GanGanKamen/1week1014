using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Character character;
    private bool verticalTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
        }
        if(Input.GetAxis("Vertical") == 0&&verticalTrigger == true)
        {
            verticalTrigger = false;
        }
    }

    private void KeyCtrl()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            character.CharacterMove(Input.GetAxis("Horizontal"));
        }
        if(Input.GetAxis("Vertical") > 0 && verticalTrigger == false)
        {

            if(character.hasFoots == true)
            {
                character.Jump();
            }
            verticalTrigger = true;
            
        }
        else if(Input.GetAxis("Vertical") < 0 && verticalTrigger == false)
        {
            if (character.onDoor != null)
            {
                character.UseDoor();
                
            }
            verticalTrigger = true;
        }
    }
}
