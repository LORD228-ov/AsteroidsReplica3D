using NUnit.Framework;
using UnityEngine;

enum Days
{
    Mon,
    Tue, 
    Wednes,
    Thue,
    Fri,
    Sat,
    Sun
};
public class test : MonoBehaviour
{
    private int animationValue = 1;
    private int dayValue = 1;
    private int[] birthDates = {1989, 2001, 1999};
    private int commonYear = 2025;

     void Start()
    {
        Animate();
        DayCheck();
        BirthSum();
    }
    void Animate()
    {
        switch (animationValue)
        {
            case 0:
                Debug.Log("IdleAnime");
                break;
            case 1:
                Debug.Log("AttackAnime");
                break;
            case 2:
                Debug.Log("WalkAnime");
                break;
            case 3:
                Debug.Log("PickUpAnime");
                break;
            case 4:
                Debug.Log("OpenDoorAnime");
                break;
            case 5:
                Debug.Log("VictoryAnime");
                break;
            default:
                Debug.Log("Shiii");
                break;
        }
    }
    void DayCheck()
    {
        switch (dayValue)
        {
            case (int)Days.Mon:
                Debug.Log("That's Monday");
                break;
            case (int)Days.Tue:
                Debug.Log("That's Tuesday");
                break;
            case (int)Days.Wednes:
                Debug.Log("That's Wednesday");
                break;
            case (int)Days.Thue:
                Debug.Log("That's Thursday");
                break;
            case (int)Days.Fri:
                Debug.Log("That's Friday");
                break;
            case (int)Days.Sat:
                Debug.Log("That's Saturday");
                break;
            case (int)Days.Sun:
                Debug.Log("That's Sunday");
                break;
            default:
                Debug.Log("Shiii");
                break;
        }
    }
    private void BirthSum()
    {
        int sum = (commonYear * 3) - birthDates[0] + birthDates[1] + birthDates[2];
        Debug.Log(sum + " is the overall age");
    }
}
