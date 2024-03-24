using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private Transform _point2;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private int _index = 0;

    Vector3 _destination;
    NavMeshAgent _agent;

    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _destination = _points[_index].position;
        _agent.destination = _destination;
    }

    void Update() {
        if(Vector3.Distance(_destination, _agent.transform.position) < 1.0f) {
            _index = (_index + 1) % _points.Count;
            _destination = _points[_index].position;
            _agent.destination = _destination;
        }
    }

    private void FixedUpdate() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _viewDistance, _mask)) {
            Debug.Log($"hit this = {hit.transform.gameObject.name}");
        } else {
            Debug.Log("hit nothing");
        }
    }
}
