using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsSystem : MonoBehaviour, IObserver
{
    [SerializeField] private Subject _player;

    void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Subject>();
    }

    private void OnEnable() {
        _player.AddObserver(this);
    }

    private void OnDisable() {
        _player.RemoveObserver(this);
    }

    public void OnNotify() {
        Debug.Log("player notified it died");
    }

}
