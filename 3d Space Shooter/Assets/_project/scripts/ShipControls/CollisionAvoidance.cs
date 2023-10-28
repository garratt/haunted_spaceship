using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _topProbe, _bottomProbe;
    [SerializeField] Transform[] _leftProbes, _rightProbes;
    [SerializeField] float _detectionRange = 100f;
    [SerializeField] bool _displayRays = false;
    
    const int AvoidLeft = 1, AvoidUp = -1, AvoidRight= -1, AvoidDown = 1, NoAvoidance = 0;

    public bool OnCollisionCourse => VerticalAvoidance != NoAvoidance || HorizontalAvoidance != NoAvoidance;
    
    public float VerticalAvoidance { get; private set; }
    public float HorizontalAvoidance { get; private set; }

    Transform _transform;

    public string _verticalCollision, _horizontalCollision;

    void Awake()
    {
        _transform = transform;
    }

    void OnEnable()
    {
        VerticalAvoidance = HorizontalAvoidance = 0f; //NoAvoidance;
    }

    void Update()
    {
        VerticalAvoidance = GetVerticalAvoidance();
        HorizontalAvoidance = GetHorizontalAvoidance();
    }

    void LateUpdate()
    {
        if (!_displayRays) return;
        Debug.DrawRay(_topProbe.position, _topProbe.forward * _detectionRange, Color.cyan);
        Debug.DrawRay(_bottomProbe.position, _bottomProbe.forward * _detectionRange, Color.yellow);
        foreach (var leftProbe in _leftProbes)
        {
            Debug.DrawRay(leftProbe.position, leftProbe.forward * _detectionRange, Color.red);
        }

        foreach (var rightProbe in _rightProbes)
        {
            Debug.DrawRay(rightProbe.position, rightProbe.forward * _detectionRange, Color.green);
        }
    }

    float GetVerticalAvoidance()
    {
        if (Physics.SphereCast(_topProbe.position, 2f, _topProbe.forward, out var hit, _detectionRange, _layerMask))
        {
            _verticalCollision = $"{_topProbe.name} detected {hit.collider.name}";

            Debug.Log($"{_topProbe.name} detected {hit.collider.name} at distance {hit.distance}");
            return 10f/(hit.distance + 1f);
            // return AvoidDown;
        }

        if (Physics.SphereCast(_bottomProbe.position, 2f, _bottomProbe.forward, out hit, _detectionRange, _layerMask))
        {
            _verticalCollision = $"{_bottomProbe.name} detected {hit.collider.name}";
            Debug.Log($"{_bottomProbe.name} detected {hit.collider.name} at distance {hit.distance}");
            return -10f/(hit.distance + 1f);
            // return AvoidUp;
        }
        return 0f; //NoAvoidance;
    }

    float GetHorizontalAvoidance()
    {
        foreach (var leftProbe in _leftProbes)
        {
            if (Physics.Raycast(leftProbe.position, leftProbe.forward, out var hit, _detectionRange, _layerMask))
            {
                _horizontalCollision = $"{leftProbe.name} detected {hit.collider.name}";
                Debug.Log($"{leftProbe.name} detected {hit.collider.name} at distance {hit.distance}");

                return 10f/(hit.distance + 1f);
//            return AvoidRight;
            }
        }
        foreach (var rightProbe in _rightProbes)
        {
            if (Physics.Raycast(rightProbe.position, rightProbe.forward, out var hit, _detectionRange, _layerMask))
            {
                _horizontalCollision = $"{rightProbe.name} detected {hit.collider.name}";
                Debug.Log($"{rightProbe.name} detected {hit.collider.name} at distance {hit.distance}");

                return -10f/(hit.distance + 1f);
    //            return AvoidLeft;
            }
        }
        return 0f; //NoAvoidance;
        
    }
}