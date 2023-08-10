using UnityEngine;
using TMPro;

public class ProjectPoints_UI : MonoBehaviour
{

    [SerializeField] private TMP_Text _projectPointsTMP;
    void Start()
    {
        _projectPointsTMP.SetText("00000");
    }

    public void fn_UpdateProjectPointsText(int newValue)
    {
        _projectPointsTMP.SetText(newValue.ToString());
    }

}
