using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SelectParent : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        if (Selection.activeGameObject == this.gameObject)
        {
            SelectThisParent();
        }
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
