using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[DrawerPriority(0, 0, 2001)]
public class AcceptDefaultMemorySOAttributeDrawerPre : OdinAttributeDrawer<AcceptDefaultMemorySOAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        CallNextDrawer(label);
        if (Property.ValueEntry.WeakSmartValue == null)
        {
            var rect = GUILayoutUtility.GetLastRect();
            var defaultMemory = DragAndDropUtilities.DropZone<DefaultMemorySO>(rect, null);
            if (defaultMemory)
            {
                Property.ValueEntry.WeakSmartValue = defaultMemory.Memory;
            }
        }
    }
}

[DrawerPriority(0, 0, 1999)]
public class AcceptDefaultMemorySOAttributeDrawerPost : OdinAttributeDrawer<AcceptDefaultMemorySOAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var rect = GUILayoutUtility.GetLastRect();
        var defaultMemory = DragAndDropUtilities.DropZone<DefaultMemorySO>(rect, null);
        if (defaultMemory)
        {
            Property.ValueEntry.WeakSmartValue = defaultMemory.Memory;
        }
        CallNextDrawer(label);
    }
}