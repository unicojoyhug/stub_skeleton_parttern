using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StubSkeletonPracticeTwoTypes.Common {
    interface IPersonChecker {
        Person Oldest(Person p1, Person p2); 
    }
}
