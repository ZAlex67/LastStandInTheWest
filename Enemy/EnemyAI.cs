using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private const int MaxOverlaps = 10;

    [SerializeField] private float _detectionRadius = 100f;
    [SerializeField] private LayerMask _layerMask;

    private Collider[] _overlapBuffer;
    private NavMeshAgent _agent;
    private float _nextSearchTime;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _overlapBuffer = new Collider[MaxOverlaps];
    }

    private void Update()
    {
        if (!_agent.enabled)
        {
            return;
        }

        if (Time.time >= _nextSearchTime)
        {
            SearchPlayer();
            _nextSearchTime = Time.time + 0.2f;
        }
    }

    private void SearchPlayer()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _overlapBuffer, _layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            Collider collider = _overlapBuffer[i];

            if (collider.TryGetComponent<Player>(out Player target))
            {
                _agent.SetDestination(target.transform.position);

                break;
            }
        }
    }
}