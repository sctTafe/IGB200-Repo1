using TMPro;
using UnityEngine;

public class DayCounter_UI : MonoBehaviour
{

    [SerializeField] private TMP_Text _dayTMP;

    // Start is called before the first frame update
    void Start()
    {
        _dayTMP.SetText("1");
    }

    public void fn_UpdateCurrentDay(int day)
    {
        _dayTMP.SetText(day.ToString());
    }
}
