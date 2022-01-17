using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnClickEnemy : MonoBehaviour
{

    [SerializeField] UnityEvent unityEvent;

    private void OnMouseDown()
    {
        unityEvent.Invoke();
    }
}
