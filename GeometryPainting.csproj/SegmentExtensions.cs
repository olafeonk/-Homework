using System;
using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        private static Dictionary<WeakReference, Segment> segments = new Dictionary<WeakReference, Segment>();
        private static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment segment, Color color)
        {
            segments[new WeakReference(segment)] = segment;
            colors[segment] = color;
        }

        public static Color GetColor(this Segment segment)
        {
            return colors.ContainsKey(segment) ? colors[segment] : Color.Black;
        }
    }
}
