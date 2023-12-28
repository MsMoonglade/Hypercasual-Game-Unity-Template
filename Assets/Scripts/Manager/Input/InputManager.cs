using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{    
    public float InputSensitivity { get; private set; } = 1.5f;

    //FOR DEBUG INPUT IN GAME VIEW
    private Vector2 oldInputPos;
    private Vector2 inputDirection;

    void Update()
    {
        //if Input in main Menu Start The Game
        if (GameManager.Instance.IsInGame || GameManager.Instance.IsInMainMenu)
        {
            InGameSwipe();
        }
    }

    private void InGameSwipe()
    {
        if (Input.touchCount > 0)
        {
            if (GameManager.Instance.IsInMainMenu)
            {
                GameManager.Instance.StartGame();
            }

            Touch touch = Input.GetTouch(0); 

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {             
                CharacterBehaviour.Instance.characterMover.HorizontalMove(new Vector3(touch.deltaPosition.x * InputSensitivity, 0, 0));
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {                   
                CharacterBehaviour.Instance.characterMover.HorizontalMove(Vector3.zero);
            }
        }
    }
    
    //DEBUG INPUT IN GAME VIEW
    /*
    //DEBUG
    private void SwipeMoving()
    {
        //start
        if (Input.GetMouseButtonDown(0))
        {
            oldInputPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        //moving
        if (Input.GetMouseButton(0))
        {
            inputDirection = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - oldInputPos).normalized;
            
            CharacterBehaviour.instance.characterMover.Move(new Vector3(inputDirection.x * m_InputSensitivity, 0, inputDirection.y));
            
            oldInputPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        //ending
        if (Input.GetMouseButtonUp(0))
        {
            oldInputPos = Vector2.zero;
            inputDirection = Vector3.zero;              
            CharacterBehaviour.instance.characterMover.Move(Vector3.zero);
        }
    }    
    */
}