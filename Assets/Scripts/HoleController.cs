using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
        
    private void Update()
    { player.transform.Translate(new Vector3(joystick.Horizontal, 0, joystick.Vertical) * (speed * Time.deltaTime));
    }
}
