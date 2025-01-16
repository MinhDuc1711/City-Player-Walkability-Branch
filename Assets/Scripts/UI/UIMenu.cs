using UnityEngine;

public abstract class UIMenu : MonoBehaviour 
{
    public abstract bool State { get; set; }
    public abstract float AnimationTime { get; }

    public abstract void SetActive(bool state);

}
