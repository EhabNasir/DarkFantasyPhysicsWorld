using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public InputActionReference xInput;
    public InputActionReference jump;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
