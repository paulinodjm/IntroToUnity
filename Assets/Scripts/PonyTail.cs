using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PonyTail : MonoBehaviour 
{
    public Transform[] PhysicsShapes;
    public Transform[] Bones;
    public Transform[] AdditionalBones;

    void LateUpdate()
    {
        for (int i=0; i<PhysicsShapes.Length && i<Bones.Length; i++)
        {
            if (PhysicsShapes[i] != null && Bones[i] != null)
            {
                Bones[i].rotation = PhysicsShapes[i].rotation * Quaternion.Euler(0, -90, 90);
            }
        }

        if (PhysicsShapes[0] == null) return;
        foreach (Transform bone in AdditionalBones)
        {
            if (bone == null) continue;

            bone.rotation = PhysicsShapes[0].rotation * Quaternion.Euler(0, -90, 90);
        }
    }
}
