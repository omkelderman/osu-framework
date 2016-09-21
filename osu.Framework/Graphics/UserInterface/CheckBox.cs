// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Drawables;
using osu.Framework.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Input;

namespace osu.Framework.Graphics.UserInterface
{
    public class CheckBox : AutoSizeContainer
    {
        public event Action<bool> CheckedChanged;

        public bool Checked => checkBoxButton.Checked;

        public string Description
        {
            get { return descriptionText.Text; }
            set { descriptionText.Text = value; }
        }

        public override bool HandleInput => true;

        private CheckBoxButton checkBoxButton;
        private SpriteText descriptionText;

        public CheckBox()
        {
            Children = new Drawable[]
            {
                new FlowContainer()
                {
                    Children = new Drawable[]
                    {
                        checkBoxButton = new CheckBoxButton()
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft
                        },
                        descriptionText = new SpriteText()
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft
                        }
                    }
                }
            };

            checkBoxButton.CheckedChanged += b => CheckedChanged?.Invoke(b);
        }

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            return checkBoxButton.TriggerMouseDown(state, args);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            return checkBoxButton.TriggerMouseUp(state, args);
        }

        protected override bool OnClick(InputState state)
        {
            return checkBoxButton.TriggerClick(state);
        }

        private class CheckBoxButton : Drawable
        {
            public event Action<bool> CheckedChanged;
            public bool Checked => button.Filled;

            private RoundedBox button;

            public CheckBoxButton()
            {
                Size = new Vector2(33, 10);

                Children = new Drawable[]
                {
                    button = new RoundedBox()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,

                        Size = new Vector2(25, 10),
                        Colour = new Color4(255, 102, 170, 255),

                        Thickness = 2,
                        Radius = 5
                    }
                };
            }

            internal void TriggerCheck()
            {
                button.Filled = !button.Filled;

                CheckedChanged?.Invoke(Checked);
            }

            protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
            {
                button.ScaleTo(0.9f, 50);
                return true;
            }

            protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
            {
                button.ScaleTo(1.0f, 50);
                return true;
            }

            protected override bool OnClick(InputState state)
            {
                TriggerCheck();
                return true;
            }
        }
    }
}
