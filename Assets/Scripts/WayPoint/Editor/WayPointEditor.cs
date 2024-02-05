using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint wayPointTarget => target as WayPoint;
    

    private void OnSceneGUI() 
    {
        Handles.color = Color.red;
        if(wayPointTarget.Puntos == null)
        {
            return;
        }

        for (int i = 0; i < wayPointTarget.Puntos.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //Crear el handle
            Vector3 puntoActual = wayPointTarget.PosicionActual + wayPointTarget.Puntos[i];
            var fmh_24_70_638412914278914671 = Quaternion.identity; Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap); 

            //Crear Texto
            GUIStyle texto = new GUIStyle();
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            Vector3 aliniamiento = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(wayPointTarget.PosicionActual + wayPointTarget.Puntos[i] + aliniamiento
            , $"{i + 1}", texto);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free move Handle");
                wayPointTarget.Puntos[i] = nuevoPunto - wayPointTarget.PosicionActual;
            }
        }
    }
}
