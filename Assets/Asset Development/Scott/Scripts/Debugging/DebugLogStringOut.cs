using UnityEngine;

public class DebugLogStringOut : MonoBehaviour
{
    [SerializeField] private bool _isEnabled = true;
    [SerializeField] private string _outputString;

    public void fn_DebugLogString()
    {
        if (_isEnabled) 
            Debug.Log(_outputString);
    }
}
