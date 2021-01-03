using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Shared.Commands
{
    public interface ICommand
    {
        bool Valid();
    }
}
