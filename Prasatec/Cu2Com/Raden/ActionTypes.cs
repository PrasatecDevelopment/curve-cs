using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prasatec.Cu2Com.Raden
{
    public enum ActionTypes : short
    {
        GeneralEvent = 0,
        PasswordChange = 1,
        SecurityQuestion = 2,
        SecurityAnswer = 3,
        PasswordReset = 4
    }
}
