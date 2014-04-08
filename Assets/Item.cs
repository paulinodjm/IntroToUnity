using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Timer))]
[AddComponentMenu("IntroToUnity/Item")]
public class Item : MonoBehaviour
{
    public int Weight = 1;

    private Timer _timer;

    protected void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.Triggered += OnTimerEnded;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;

        other.gameObject.GetComponent<PlayerController>().Score += Weight;
        _timer.enabled = true;
        SetPickable(false);
    }

    private void OnTimerEnded()
    {
        SetPickable(true);
    }

    private void SetPickable(bool enabled)
    {
        renderer.enabled = enabled;
        collider.enabled = enabled;
    }
}
