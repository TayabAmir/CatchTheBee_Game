﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Catch_The_Bee.Movement
{
    public interface IMovement
    {
        Point move(Point location);
    }
}
