// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Framework.Graphics.Drawables
{
    public class RoundedBox : Box
    {
        public bool Filled;
        public float Thickness;
        public float Radius;

        private QuadBatch<Vertex2d> quadBatch = new QuadBatch<Vertex2d>(1, 3);

        protected override DrawNode BaseDrawNode => new RoundedBoxDrawNode(DrawInfo, Game, ScreenSpaceDrawQuad, quadBatch, Filled, Thickness, Radius);
    }
}
