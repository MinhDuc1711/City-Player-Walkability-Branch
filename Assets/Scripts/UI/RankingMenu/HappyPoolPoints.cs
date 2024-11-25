using UnityEngine;
using UnityEngine.UI;


public class HappyPoolPoints : MonoBehaviour
{
    public Text remainingPoints;

    private int pool = 100;

    private int used = 0;

    public const int MAX_POINTS = 100;

    public int Used
    {
        get { return used; }
    }

    public int Pool { 
        get { return pool; } 
    }

    public bool getPoints (int points)
    {
        if (points > (pool - used))
            return false;

        used = points + used;
        return true;
    }

    public bool returnPoints (int points)
    {
        if ((points + used) > MAX_POINTS)
            return false;

        used = used - points;
        return true;
    }

    private void Update ()
    {
        remainingPoints.text = (pool-used).ToString();
    }


}
