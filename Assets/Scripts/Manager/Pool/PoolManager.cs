using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonPersistent<PoolManager>
{    
    private List<PooledObject> pool = new List<PooledObject>();

    [HideInInspector]
    public List<GameObject> poolParents = new List<GameObject>();  


    protected override void Awake()
    {
        base.Awake();

        foreach (PooledObject p in pool)
        {
            GameObject poolParent = new GameObject(p.obj_Parent_Name);
            poolParent.transform.parent = transform;

            GeneratePool(p.obj_Prefs, p.initial_quantity, poolParent);
        }
    }

    private void GeneratePool(GameObject i_obj, int i_quantity, GameObject i_parent)
    {
        for (int i = 0; i < i_quantity; i++)
        {
            GameObject o = InstantiateInPool(i_obj, i_parent);
        }

        poolParents.Add(i_parent);
    }

    public GameObject GetPooledItem(GameObject i_obj, Vector3 i_pos)
    {
        GameObject o_obj = null;

        GameObject objParent = null;

        for (int i = 0; i < pool.Count; i++)
        {
            if (i_obj == pool[i].obj_Prefs)
            {
                objParent = poolParents[i];
                break;
            }
        }
        if(objParent == null)
        {
            Debug.LogWarning("NoItemInPool");
            return null;
        }

        o_obj = PickObjectFromParent(objParent);

        if (o_obj != null)
        {
            o_obj.transform.position = i_pos;
            o_obj.gameObject.SetActive(true);
            return o_obj;
        }

        else
        {
            GameObject o_newObj = InstantiateInPool(i_obj, objParent);
            o_newObj.transform.position = i_pos;
            o_newObj.gameObject.SetActive(true);
            return o_newObj;
        }
    }

    private GameObject PickObjectFromParent(GameObject i_parent)
    {
        GameObject o_pooledObj = null;

        foreach(Transform child in i_parent.transform)
        {           
            if (!child.gameObject.activeInHierarchy)
            {
                o_pooledObj = child.gameObject;
            }
        }

        return o_pooledObj;
    }

    private GameObject InstantiateInPool(GameObject i_obj, GameObject i_parent)
    {
        GameObject o_inst = Instantiate(i_obj, i_parent.transform.position, i_obj.transform.rotation, i_parent.transform);
        o_inst.gameObject.SetActive(false);

        return o_inst;
    }
}

[System.Serializable]
public struct PooledObject
{
    public GameObject obj_Prefs;
    public string obj_Parent_Name;
    public int initial_quantity;
}