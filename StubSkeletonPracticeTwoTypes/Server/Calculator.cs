﻿using StubSkeletonPracticeTwoTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes.Server {
    internal class Calculator : ICalcaulator {
        public int Add(int x, int y) {
            return x + y;
        }
    }
}
