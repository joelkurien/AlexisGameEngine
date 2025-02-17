using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI
{
    public class FileConstants
    {
        public static string HomePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AlexisEditor");
    }
}
