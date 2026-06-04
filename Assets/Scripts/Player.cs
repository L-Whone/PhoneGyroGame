using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private string hitTag;

    public UnityEvent OnDeath;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitTag)
        {
            OnDeath.Invoke();
        }
    }
}
