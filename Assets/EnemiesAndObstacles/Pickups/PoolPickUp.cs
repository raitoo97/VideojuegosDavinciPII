using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Enum para mas de un Pickup Item
public enum PickupType
{
   Xp,
}
*/ 

public class PoolPickUp : MonoBehaviour
{
    public static PoolPickUp instance;
    public List<PoolPickUpsStruct> poolPickUpsStructs = new List<PoolPickUpsStruct>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitPool());
    }

    IEnumerator WaitPool()
    {
        yield return new WaitForEndOfFrame();
        foreach (var item in poolPickUpsStructs)
        {
            item.OnStart();
        }
    }

    [Serializable]
    public class PoolPickUpsStruct
    {
        public int initList;
        public float points = 25f;
        private float chance = 100f;
        private bool dropped;
        [SerializeField] GameObject _prefab;
        [SerializeField] private List<GameObject> _itemPool = new List<GameObject>();
        

        public void OnStart()
        {
            CompleteList(initList);
        }
    
        public void CompleteList(int initList)
        {
            for(int i = 0; i < initList; i++)
            {
                var _clonedItem = GameObject.Instantiate(_prefab);
                _clonedItem.SetActive(false);
                _itemPool.Add(_clonedItem);

            }
        }


        public void Drop(Transform parent)
        {
            dropped = (UnityEngine.Random.Range(0, chance)) >= 50;

            if (dropped)
            {

                var _clonedPrefab = GetItem();
                _clonedPrefab.transform.position = parent.position;
                
            }
        }


        public GameObject GetItem()
        {
            for (int i = 0; i < _itemPool.Count; i++)
            {
                if (!_itemPool[i].activeSelf)
                {
                    _itemPool[i].SetActive(true); return _itemPool[i];
                }
            }

            CompleteList(1);
            GameObject _auxItem = _itemPool[_itemPool.Count - 1];
            
            _auxItem.SetActive(true); return _auxItem;
        }

    
    
    }
    
    
}
