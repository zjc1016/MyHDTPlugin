﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHDTPlugin
{
    interface IGameMonitor : IUpdateable
    {
        void Initialize();
    }
}
