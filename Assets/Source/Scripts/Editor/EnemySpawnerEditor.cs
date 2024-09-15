using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(EnemySpawner enemySpawner, GizmoType type)
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(enemySpawner.transform.position, 0.5f);
    }
}