using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Keybinds
{
    private static Dictionary<Actions, KeyCode> keyBinds = new Dictionary<Actions, KeyCode>();
    public static Dictionary<Actions, KeyCode> KeyBinds { get => keyBinds; }

    public static void SetDefaultKeybinds() 
    {
        keyBinds.Clear();

        keyBinds.Add(Actions.Up, KeyCode.W);
        keyBinds.Add(Actions.Down, KeyCode.S);
        keyBinds.Add(Actions.Right, KeyCode.D);
        keyBinds.Add(Actions.Left, KeyCode.A);

        keyBinds.Add(Actions.Dash, KeyCode.LeftControl);

        keyBinds.Add(Actions.Use, KeyCode.E);

        keyBinds.Add(Actions.Attack1, KeyCode.Mouse0);
        keyBinds.Add(Actions.Attack2, KeyCode.Mouse1);

        keyBinds.Add(Actions.Codex, KeyCode.J);
        keyBinds.Add(Actions.Map, KeyCode.M);
    }

    public static void BindAKey(Actions action, KeyCode keyCode)
    {
        keyBinds.Remove(action);
        keyBinds.Add(action, keyCode);
    }

    public static float GetAxisRaw(Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            if (Input.GetKey(keyBinds[Actions.Right]) && Input.GetKey(keyBinds[Actions.Left])) return 0f;

            if (Input.GetKey(keyBinds[Actions.Right]))
            {
                return 1f;
            }
            else if (Input.GetKey(keyBinds[Actions.Left]))
            {
                return -1f;
            }

            else return 0f;
        }

        else if (axis == Axis.Vertical)
        {
            if (Input.GetKey(keyBinds[Actions.Up]) && Input.GetKey(keyBinds[Actions.Down])) return 0f;

            if (Input.GetKey(keyBinds[Actions.Up]))
            {
                return 1f;
            }
            else if(Input.GetKey(keyBinds[Actions.Down]))
            {
                return -1f;
            }

            else return 0f;
        }

        else return 0f;
    }
}

public enum Actions
{
    Up,
    Down,
    Right,
    Left,
    Dash,
    Use,
    Attack1,
    Attack2,
    Codex,
    Map,
}

public enum Axis
{
    Horizontal,
    Vertical
}
