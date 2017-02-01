using StubSkeletonPracticeTwoTypes.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes.Server {
    class PersonChecker : IPersonChecker {
        public Person Oldest(Person p1, Person p2) {
            if (p1.CompareTo(p2) > 0)
                return p1;
            else
                return p2;
        }
    }
}

