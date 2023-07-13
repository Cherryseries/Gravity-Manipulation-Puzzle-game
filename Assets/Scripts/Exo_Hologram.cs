using UnityEngine;

public class Exo_Hologram : MonoBehaviour
{
    public static Exo_Hologram instance;

    private void Awake()
    {
        instance = this;
    }
    
    public int rotation_val;

    //hologram front and back rotation
    public void HologramRotationLeftRight(int rotateVal) 
    {
        transform.eulerAngles = new Vector3(0, 0, rotateVal);
        rotation_val = rotateVal;
    }
    //hologram front and back rotation
    public void HologramRotationFrontBack(int rotateVal) 
    {
        transform.eulerAngles = new Vector3(rotateVal,0,0);
        rotation_val = rotateVal;
    }
    public void HologramHide() { gameObject.SetActive(false); }  // hide hologram
    public void HologramShow() { gameObject.SetActive(true); }   // show hologram
    
}

