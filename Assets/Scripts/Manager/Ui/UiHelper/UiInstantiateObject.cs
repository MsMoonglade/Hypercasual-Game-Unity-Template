using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UiManager))]
public class UiInstantiateObject : MonoBehaviour
{
    public GameObject uiTempParent;

    public List<UiElementToInstantiate> elementToInstantiate = new List<UiElementToInstantiate>();

    public void InstantiateElementInUi(int i_quantity , int i_index)
    {
        Vector3 pos = elementToInstantiate[i_index].obj_Pos;
        Vector3 dest = elementToInstantiate[i_index].obj_Destination.transform.position;
        float animTime = elementToInstantiate[i_index].animTime;

        GameObject obj = elementToInstantiate[i_index].obj_Prefs;

        for (int i = 0; i < i_quantity ; i++)
        {
            GameObject coin = Instantiate(obj , pos, Quaternion.identity, transform);           
            coin.transform.localScale = Vector3.zero;

            AnimateInstantiatedElement(coin, dest, animTime);
        }
    }

    public void InstantiateElementInUi_ObjPos(GameObject i_objPos, int i_quantity, int i_index)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(i_objPos.transform.position);
        Vector3 dest = elementToInstantiate[i_index].obj_Destination.transform.position;
        float animTime = elementToInstantiate[i_index].animTime;

        GameObject obj = elementToInstantiate[i_index].obj_Prefs;

        for (int i = 0; i < i_quantity; i++)
        {
            GameObject coin = Instantiate(obj, pos, Quaternion.identity, uiTempParent.transform);
            coin.transform.localScale = Vector3.zero;

            AnimateInstantiatedElement(coin, dest, animTime);
        }
    }

    private void AnimateInstantiatedElement(GameObject i_obj , Vector3 i_dest , float i_animTime)
    {
        i_obj.transform.DOScale(Vector3.one, 0.2f)
           .SetEase(Ease.OutBack);

        i_obj.transform.DOMove(i_dest , i_animTime)
            .SetEase(Ease.InBack)
            .OnComplete(() => Destroy(i_obj));
    }
}

[System.Serializable]
public struct UiElementToInstantiate
{
    public GameObject obj_Prefs;    
    public Vector3 obj_Pos;
    public GameObject obj_Destination;
    public float animTime;
}
