/*using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(Test))]
public class TriggerContainerEditor : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var amountField = new PropertyField(property.FindPropertyRelative("amount"));
        var unitField = new PropertyField(property.FindPropertyRelative("unit"));
        var nameField = new PropertyField(property.FindPropertyRelative("name"), "Fancy Name");

        // Add fields to the container.
        container.Add(amountField);
        container.Add(unitField);
        container.Add(nameField);

        return container;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Segments");

        // keep the originals
        float lw = EditorGUIUtility.labelWidth;
        TextClipping tc = EditorStyles.label.clipping;
        RectOffset bord = EditorStyles.label.border;
        RectOffset pad = EditorStyles.label.padding;

        // alter values
        EditorGUIUtility.labelWidth = 8f;
        EditorStyles.label.clipping = TextClipping.Overflow;
        EditorStyles.label.border = new RectOffset(0, 0, 0, 0);
        EditorStyles.label.padding = new RectOffset(0, 0, 0, 0);

        EditorGUILayout.PropertyField(property.FindPropertyRelative("_serialisedName"));

        // no one saw something
        EditorGUIUtility.labelWidth = lw;
        EditorStyles.label.clipping = tc;
        EditorStyles.label.border = bord;
        EditorStyles.label.padding = pad;

        EditorGUILayout.EndHorizontal();


        SerializedProperty serialisedList = property.FindPropertyRelative("_serialisedName");
        if (originalPosition.Contains(UnityEngine.Event.current.mousePosition))
        {
            switch (UnityEngine.Event.current.type)
            {
                case EventType.DragUpdated:
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    UnityEngine.Event.current.Use();
                    break;
                case EventType.DragPerform:
                    DragAndDrop.AcceptDrag();
                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is GameObject addedGameObject)
                        {
                            Test addedItem = addedGameObject.GetComponent<Test>();
                            if (addedItem != null)
                            {
                                int index = serialisedList.arraySize;
                                serialisedList.InsertArrayElementAtIndex(index);
                                serialisedList.GetArrayElementAtIndex(index).objectReferenceValue = addedItem;
                            }
                        }
                    }
                    break;
            }
        }
    }
}*/