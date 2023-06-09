using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LiveList<T>
{
    private LinkedList<T> list;
    public LiveList()
    {
        list = new LinkedList<T>();
    }
    public void Add(T obj)
    {
        list.AddFirst(obj);
    }
    public void Remove(T obj)
    {
        try
        {
            list.Remove(obj);
        }
        catch (System.InvalidOperationException)
        {
            // ignore
        }
    }
    public T GetOldest()
    {
        // remove the oldest from the list and return it, return null if empty
        T retVal = default(T);
        if (list.Last != null)
        {
            retVal = list.Last.Value;
            list.RemoveLast();
        }
        return retVal;
    }
}
public class ObjectPool : MonoBehaviour
{
    public enum Policy
    {
        RETURN_NULL,
        REPLACE_OLDEST
    };
    [SerializeField] Policy policy;
    [SerializeField] int poolSize;
    [SerializeField] Transform prefab;
    private GameObject[] pool;
    LinkedList<GameObject> freeList;
    LiveList<GameObject> liveList;
    public GameObject hunger;
    public GameObject strength;
    public GameObject stamina;
    public GameObject thirst;
    // Start is called before the first frame update
    void Start()
    {
        if (policy == Policy.REPLACE_OLDEST)
        {
            liveList = new LiveList<GameObject>();
        }
        freeList = new LinkedList<GameObject>();

        // initialize the pool and spawn the objects
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            //pool[i] = Instantiate(hunger).gameObject;
            //pool[i] = Instantiate(strength).gameObject;
            //pool[i] = Instantiate(stamina).gameObject;
            //pool[i] = Instantiate(thirst).gameObject;
            //pool[i].SetActive(false);

            freeList.AddFirst(pool[i]);
        }
    }

    public GameObject GetObject()
    {
        // TODO: Look for an unused object and return the first one found
        //for(int i =0; i < poolSize; i++)
        //{
        //    if (!pool[i].activeInHierarchy)
        //    {
        //        return pool[i];
        //    }
        //}
        //return null;
        if(freeList.First != null)
        {
            GameObject obj = freeList.First.Value;
            freeList.RemoveFirst();
            if(policy == Policy.REPLACE_OLDEST)
            {
                liveList.Add(obj);

            }
            return obj;
        }
        else
        {
            if(policy == Policy.REPLACE_OLDEST)
            {
                GameObject obj = liveList.GetOldest();
                liveList.Add(obj);
                return obj;
            }
        }
        return null;
    }

    public void ReturnObject(GameObject o)
    {
        // TODO: set this object to be unused
        o.SetActive(false);
        freeList.AddFirst(o);
        if (policy == Policy.REPLACE_OLDEST)
        {
            liveList.Remove(o);
        }
    }
}
