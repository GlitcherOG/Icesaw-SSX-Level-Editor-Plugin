using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectParent : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        SelectThisParent();
    }

    public void SelectThisParent()
    {
        if(transform.parent.GetComponent<SelectParent>()!=null)
        {
            transform.parent.GetComponent<SelectParent>().SelectThisParent();
        }
        else
        {
            Selection.activeGameObject = transform.parent.gameObject;
        }
    }
}
