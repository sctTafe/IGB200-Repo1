using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressionInteractableObjects_SupportScript : MonoBehaviour
{
    private GameProgressionInteractableObjects_PersistentSingletonMng gameProgressionInteractableObjectsMng;
    // Start is called before the first frame update
    void Start()
    {
        gameProgressionInteractableObjectsMng = GameProgressionInteractableObjects_PersistentSingletonMng.Instance;
    }


    public void fn_EnableBuildingByID(int id)
    {
        gameProgressionInteractableObjectsMng.fn_SetBridgeObjectToEnabled(id);
    }
}
