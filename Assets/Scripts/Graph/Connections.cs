using Edgar.Unity;
using UnityEngine;

namespace GraphSystem
{
    public class Connections : Connection
    {
        public bool IsLocked;

        public override ConnectionEditorStyle GetEditorStyle(bool isFocused)
        {
            var style = base.GetEditorStyle(isFocused);

            if (IsLocked)
            {
                style.LineColor = Color.red;
            }

            return style;
        }
    }
}

