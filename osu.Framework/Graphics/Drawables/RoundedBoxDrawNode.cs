// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using OpenTK;
using osu.Framework.Graphics.Batches;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;

namespace osu.Framework.Graphics.Drawables
{
    public class RoundedBoxDrawNode : DrawNode
    {
        private static Shader shader;
        private static Uniform<float> thicknessUniform;
        private static Uniform<float> radiusUniform;
        private static Uniform<bool> filledUniform;
        private static Uniform<Vector2> centerPositionUniform;
        private static Uniform<Vector2> sizeUniform;

        private Game game;
        private Quad screenSpaceDrawQuad;
        private QuadBatch<Vertex2d> batch;

        private float thickness;
        private bool filled;
        private float radius;

        public RoundedBoxDrawNode(DrawInfo drawInfo, Game game, Quad screenSpaceDrawQuad, QuadBatch<Vertex2d> batch, bool filled, float thickness, float radius)
            : base(drawInfo)
        {
            this.game = game;
            this.screenSpaceDrawQuad = screenSpaceDrawQuad;
            this.batch = batch;
            this.filled = filled;
            this.thickness = thickness;
            this.radius = radius;
        }

        protected override void Draw()
        {
            base.Draw();

            if (shader == null)
            {
                shader = game.Shaders.Load(VertexShader.PositionColour, FragmentShader.RoundedBox);
                thicknessUniform = shader.GetUniform<float>("m_thickness");
                radiusUniform = shader.GetUniform<float>("m_radius");
                filledUniform = shader.GetUniform<bool>("m_filled");
                centerPositionUniform = shader.GetUniform<Vector2>("m_centerpos");
                sizeUniform = shader.GetUniform<Vector2>("m_size");
            }

            shader.Bind();

            thicknessUniform.Value = thickness;
            radiusUniform.Value = radius;
            filledUniform.Value = filled;
            centerPositionUniform.Value = screenSpaceDrawQuad.Centre;
            sizeUniform.Value = screenSpaceDrawQuad.Size;

            batch.Add(new Vertex2d
            {
                Colour = DrawInfo.Colour,
                Position = screenSpaceDrawQuad.BottomLeft
            });
            batch.Add(new Vertex2d
            {
                Colour = DrawInfo.Colour,
                Position = screenSpaceDrawQuad.BottomRight
            });
            batch.Add(new Vertex2d
            {
                Colour = DrawInfo.Colour,
                Position = screenSpaceDrawQuad.TopRight
            });
            batch.Add(new Vertex2d
            {
                Colour = DrawInfo.Colour,
                Position = screenSpaceDrawQuad.TopLeft
            });
            batch.Draw();

            shader.Unbind();
        }
    }
}
