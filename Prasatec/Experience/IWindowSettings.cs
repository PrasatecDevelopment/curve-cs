﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Experience
{
    public interface IWindowSettings : IWindow
    {
        new IWindow Base { get; }
    }
}
