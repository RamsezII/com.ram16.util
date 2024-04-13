using UnityEngine;
using UnityEngine.UI;

namespace _UTIL_
{
    [RequireComponent(typeof(CanvasRenderer))]
    internal class EmptyGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh) => vh.Clear();
    }
}