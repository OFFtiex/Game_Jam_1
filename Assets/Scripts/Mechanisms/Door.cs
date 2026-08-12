using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator DoorAnimator;
    public BoxCollider2D BoxCollider;
    bool isFoolOpen = false;
    private void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    public void Open()
    {
        if (DoorAnimator != null) { DoorAnimator.SetTrigger("Open"); }

        if (BoxCollider != null)  { BoxCollider.isTrigger = true;    }
    }
    public void FullOpen() 
    { 
        isFoolOpen=true;
        Open();
    }
    public void Close()
    {
        if (isFoolOpen) { 
            Debug.Log("The door is stuck open!"); 
            return; 
        }

        if (DoorAnimator != null) { DoorAnimator.SetTrigger("Close"); }

        if (BoxCollider != null)  { BoxCollider.isTrigger = false;    }
    }
}
