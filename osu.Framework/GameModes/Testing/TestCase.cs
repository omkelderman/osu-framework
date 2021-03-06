﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Allocation;

namespace osu.Framework.GameModes.Testing
{
    public abstract class TestCase : Container
    {
        public override string Name => @"Test Case";
        public virtual string Description => @"The base class for a test case";

        FlowContainer buttonsContainer;

        // TODO: Figure out how to make this private (e.g. through reflection).
        //       Right now this is required for DrawVis to inspect the Drawable tree.
        public Container Contents;

        protected override Container<Drawable> Content => Contents;
        
        protected DependencyContainer Dependencies { get; private set; }

        public TestCase()
        {
            RelativeSizeAxes = Axes.Both;
            Masking = true;
        }

        [BackgroundDependencyLoader]
        private void load(DependencyContainer deps)
        {
            Dependencies = deps;
        }

        public virtual void Reset()
        {
            if (Contents == null)
            {
                InternalChildren = new Drawable[]
                {
                    new ScrollContainer
                    {
                        Children = new[]
                        {
                            buttonsContainer = new FlowContainer
                            {
                                Direction = FlowDirection.VerticalOnly,
                                AutoSizeAxes = Axes.Both,
                                Spacing = new Vector2(15, 5)
                            }
                        }
                    },
                    Contents = new Container()
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                };
            }
            else
            {
                Contents.Clear();
                buttonsContainer.Clear();
            }
        }

        public Button AddButton(string text, Action action)
        {
            Button b;
            buttonsContainer.Add(b = new Button
            {
                Colour = Color4.LightBlue,
                Size = new Vector2(150, 50),
                Text = text
            });

            b.Action += action;

            return b;
        }

        public void AddToggle(string text, Action action)
        {
            buttonsContainer.Add(new ToggleButton(action)
            {
                Text = text
            });
        }
    }

    public class ToggleButton : Button
    {
        private readonly Action reloadCallback;
        private static Color4 offColour = Color4.Red;
        private static Color4 onColour = Color4.YellowGreen;

        public bool State;

        public ToggleButton(Action reloadCallback)
        {
            this.reloadCallback = reloadCallback;

            Size = new Vector2(100, 50);
            Colour = offColour;
            Action += clickAction;
        }

        private void clickAction()
        {
            State = !State;
            Colour = State ? onColour : offColour;
            reloadCallback?.Invoke();
        }
    }
}
