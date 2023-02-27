using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[DrawerPriority(0, 0, 2001)]
public class AcceptDefaultMemorySOAttributeDrawerPre : OdinAttributeDrawer<AcceptDefaultMemorySOAttribute>
{
    private bool IsRootList => !Property.ParentType.ImplementsOpenGenericInterface(typeof(IList)) && Property.Info.TypeOfValue.ImplementsOpenGenericInterface(typeof(IList));
    protected override void DrawPropertyLayout(GUIContent label)
    {
        CallNextDrawer(label);
        if (Property.ValueEntry.WeakSmartValue == null || IsRootList)
        {
            var rect = GUILayoutUtility.GetLastRect();
            var defaultMemory = DragAndDropUtilities.DropZone<DefaultMemorySO>(rect, null);
            if (defaultMemory)
            {
                if (Property.Info.TypeOfValue.ImplementsOpenGenericInterface(typeof(IList)))
                {
                    (Property.ValueEntry.WeakSmartValue as IList).Add(defaultMemory.Memory);
                }
                else
                {
                    Property.ValueEntry.WeakSmartValue = defaultMemory.Memory;
                }
            }
        }
    }
}

[DrawerPriority(0, 0, 1999)]
public class AcceptDefaultMemorySOAttributeDrawerPost : OdinAttributeDrawer<AcceptDefaultMemorySOAttribute>
{
    private bool IsRootList => !Property.ParentType.ImplementsOpenGenericInterface(typeof(IList)) && Property.Info.TypeOfValue.ImplementsOpenGenericInterface(typeof(IList));
    protected override void DrawPropertyLayout(GUIContent label)
    {
        if (!IsRootList)
        {
            var rect = GUILayoutUtility.GetLastRect();
            var defaultMemory = DragAndDropUtilities.DropZone<DefaultMemorySO>(rect, null);
            if (defaultMemory)
            {
                if (Property.Info.TypeOfValue.ImplementsOpenGenericInterface(typeof(IList)))
                {
                    (Property.ValueEntry.WeakSmartValue as IList).Add(defaultMemory.Memory);
                }
                else
                {
                    Property.ValueEntry.WeakSmartValue = defaultMemory.Memory;
                }
            }
        }
        CallNextDrawer(label);
    }
}