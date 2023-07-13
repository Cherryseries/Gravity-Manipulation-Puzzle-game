using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject hologram;   // hologram
    public event EventHandler OnJumpEvent;
    public event EventHandler OnManipulateEvent;
    private PlayerExoAction playerInputs;      //input system

    private void Awake()
    {
        playerInputs = new PlayerExoAction();
        playerInputs.Player.Enable();
        playerInputs.Player.Jump.performed += Jump_performed;
        playerInputs.Player.Manipulation.performed += Manipulation_performed;
        playerInputs.Player.Hologramup.performed += Hologramup_performed;
        playerInputs.Player.HologramDown.performed += HologramDown_performed;
        playerInputs.Player.HologramRight.performed += HologramRight_performed;
        playerInputs.Player.HologramLeft.performed += HologramLeft_performed;
    }
    //hologram rotation (Left arrow key)
    private void HologramLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
            hologram.SetActive(true);

            if (Exo_Hologram.instance.rotation_val == 0)
            {
                Exo_Hologram.instance.HologramRotationLeftRight(90);

            }
            else if (Exo_Hologram.instance.rotation_val == 90)
            {

                Exo_Hologram.instance.HologramRotationLeftRight(180);

            }
            else if (Exo_Hologram.instance.rotation_val == 180)
            {

                Exo_Hologram.instance.HologramRotationLeftRight(270);

            }
            else if (Exo_Hologram.instance.rotation_val == 270)
            {
                Exo_Hologram.instance.HologramRotationLeftRight(0);
            }
        
    }
    //hologram rotation (Right arrow key)
    private void HologramRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        hologram.SetActive(true);
        if (Exo_Hologram.instance.rotation_val == 0)
        {
            Exo_Hologram.instance.HologramRotationLeftRight(270);

        }
        else if (Exo_Hologram.instance.rotation_val == 270)
        {

            Exo_Hologram.instance.HologramRotationLeftRight(180);

        }
        else if (Exo_Hologram.instance.rotation_val == 180)
        {

            Exo_Hologram.instance.HologramRotationLeftRight(90);

        }
        else if (Exo_Hologram.instance.rotation_val == 90)
        {

            Exo_Hologram.instance.HologramRotationLeftRight(0);

        }
    }
    //hologram rotation (down arrow key)
    private void HologramDown_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        hologram.SetActive(true);
        if (Exo_Hologram.instance.rotation_val == 0)
        {
            Exo_Hologram.instance.HologramRotationFrontBack(270);

        }
       else if (Exo_Hologram.instance.rotation_val == 270)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(180);

        }
        else if (Exo_Hologram.instance.rotation_val == 180)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(90);

        }
        else if (Exo_Hologram.instance.rotation_val == 90)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(0);
        }
    }
    //hologram rotation (up arrow key)
    private void Hologramup_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        hologram.SetActive(true);
        if (Exo_Hologram.instance.rotation_val == 0)
        {
            Exo_Hologram.instance.HologramRotationFrontBack(90);

        }
        else if (Exo_Hologram.instance.rotation_val == 90)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(180);

        }
        else if (Exo_Hologram.instance.rotation_val == 180)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(270);

        }
        else if (Exo_Hologram.instance.rotation_val == 270)
        {

            Exo_Hologram.instance.HologramRotationFrontBack(0);
        }
    
    }
    private void Manipulation_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnManipulateEvent?.Invoke(this, EventArgs.Empty); // perform manipulation (enter key)
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpEvent?.Invoke(this, EventArgs.Empty); //perform the jump event (space key)
    }
    
    public Vector2 GetPlayerMomentNormalized()  // return the WASD values
    {
        Vector2 inputVector = playerInputs.Player.Moment.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }   
}
