using UnityEngine;
using System.Collections;

public class CubeBehavior : Bolt.EntityBehaviour<ICubeState>
{
    protected Joystick joystick;

    public override void Attached()
    {
        joystick = FindObjectOfType<Joystick>();
        state.SetTransforms(state.CubeTransform, transform);
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;

        movement.x += joystick.Horizontal;
        movement.z += joystick.Vertical;

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.frameDeltaTime);
        }
    }
}