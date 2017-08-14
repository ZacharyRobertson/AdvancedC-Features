using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Preprocessor Directives
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AbstractClasses
{
    public class Shotgun : Weapon
    {
        public float shootAngle = 45f;
        public float shootRadius = 5f;

        public Vector3 GetDir(float angleD)
        {
            float anglerR = angleD * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(anglerR), Mathf.Sin(anglerR));
            return transform.rotation * dir;
        }
        public override void Fire()
        {
            
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Shotgun))]
    public class ShotgunEditor: Editor
    {
        void OnSceneGUI()
        {
            Shotgun shotgun = (Shotgun)target;

            Transform transform = shotgun.transform;
            Vector2 pos = transform.position;

            float angle = shotgun.shootAngle;
            float radius = shotgun.shootRadius;

            Vector2 leftDir = shotgun.GetDir(angle);
            Vector2 rightDir = shotgun.GetDir(-angle);

            Handles.color = Color.red;
            Handles.DrawLine(pos, pos + leftDir * radius);
            Handles.DrawLine(pos, pos + rightDir * radius);

            Handles.color = Color.cyan;
            Handles.DrawWireArc(pos, Vector3.forward, rightDir, angle * 2, radius);
        }
    }
#endif
}