using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olimpo_XIII.Interface
{
    interface IUser
    {
        abstract dynamic passEquals();
        abstract bool CheckUser();
        abstract dynamic CheckLogin();
        abstract dynamic CheckPass(string value,string title);
    }
}
