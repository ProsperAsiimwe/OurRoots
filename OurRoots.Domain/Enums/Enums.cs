using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurRoots.Domain.Enums
{
   public enum Status: int
    {
        Pending = 1,
        Dispatched =2,
        Delivered = 3,
        Returned = 4
    }
}
