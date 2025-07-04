using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PickupType
{
   Xp,
   Health
}
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
        [SerializeField] GameObject _prefab;
        public PickupType type;
        public int initList;
        //[SerializeField] public float points;
        [SerializeField] float chance;
        private bool dropped;
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
        public void Drop(Vector3 position)
        {
            dropped = (UnityEngine.Random.Range(0f, 100f)) <= chance;
            if (dropped)
            {
                var _clonedPrefab = GetItem();
                _clonedPrefab.transform.position = position;
            }
        }
        public GameObject GetItem()
        {
            foreach (var item in _itemPool)
            {
                if (!item.activeSelf)
                {
                    item.SetActive(true);
                    //AssignBehaviorValues(item);
                    return item;
                }
            }
            CompleteList(1);
            GameObject newItem = _itemPool[_itemPool.Count - 1];
            newItem.SetActive(true);
            //AssignBehaviorValues(newItem);
            return newItem;
        }
        /*
        private void AssignBehaviorValues(GameObject item)
        {
            Debug.Log($"[DEBUG] Asignando puntos: {points} al item {type}");

            if (type == PickupType.Health && item.TryGetComponent<itemHealthBehavior>(out var heal))
            {
                heal.SetHealingPoints(points);
            }

            if (type == PickupType.Xp && item.TryGetComponent<itemXPBehavior> (out var xp))
            {
                xp.SetXpPoints(points);
            }
        }*/
    }
}
