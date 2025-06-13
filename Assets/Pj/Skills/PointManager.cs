    using UnityEngine;
    public class PointManager : MonoBehaviour
    {
        [SerializeField] private float _currentPoints;
        public static PointManager instance;
        private HandleEnemyPoints HandelEnemy;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
        private void Start()
        {
            HandelEnemy = new HandleEnemyPoints();
        }
        public void AddPoints(float value)
        {
            _currentPoints += value;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _currentPoints += 1000;
            }
        }
        public bool SpendPoints(float cost)
        {
            if(_currentPoints >= cost)
            {
                _currentPoints -= cost;
                return true;
            }
            else
            {
                return false;
            }
        }
        public float GetPoints => _currentPoints;
        public HandleEnemyPoints GetHandle => HandelEnemy;
    }
