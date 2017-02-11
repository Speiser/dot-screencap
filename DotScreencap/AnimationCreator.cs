using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotScreencap
{
    internal static class AnimationCreator
    {
        private static ScreenCapture _screencap;

        public static void CreateAnimation(ScreenCapture sc)
        {
            _screencap = sc;
        }
    }
}
