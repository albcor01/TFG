using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


// IngredientDrawer
[CustomPropertyDrawer(typeof(MM.MusicInput))]
public class MusicInputDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var objRect = new Rect(position.x, position.y, 70, position.height);
        var compRect = new Rect(position.x + 75, position.y, 70, position.height);
        var varRect = new Rect(position.x + 150, position.y, 70, position.height);
        var minRect = new Rect(position.x + 225, position.y, 35, position.height);
        var maxRect = new Rect(position.x + 265, position.y, 35, position.height);


        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(objRect, property.FindPropertyRelative("objeto"), GUIContent.none);
        EditorGUI.PropertyField(compRect, property.FindPropertyRelative("componente"), GUIContent.none);
        EditorGUI.PropertyField(varRect, property.FindPropertyRelative("variable"), GUIContent.none);
        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), GUIContent.none);
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);

        ////Solo dibujamos el min y max si no se trata de un booleano
        //SerializedProperty prop = property.FindPropertyRelative("variable");
        //object value = MM.Utils.getInputValue(prop.stringValue);
        //Debug.Log(value);
        //if (prop.propertyType != SerializedPropertyType.Boolean)
        //{
        //    EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), GUIContent.none);
        //    EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);
        //}
        //MM.MusicInput target = (MM.MusicInput)fieldInfo.GetValue(property);
        //Debug.Log(MM.Utils.getPropertyType(target).ToString());

        //if (MM.Utils.getPropertyType(target).ToString())
        //{
        //    EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), GUIContent.none);
        //    EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);
        //}

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
