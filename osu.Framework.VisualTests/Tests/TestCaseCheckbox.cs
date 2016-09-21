// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Framework.VisualTests.Tests
{
    class TestCaseCheckbox : TestCase
    {
        internal override string Name => @"Checkbox";
        internal override string Description => @"Checkboxes";

        internal override void Reset()
        {
            base.Reset();

            CheckBox c;
            Add(c = new CheckBox()
            {
                Description = "Hello world?",
            });
        }
    }
}