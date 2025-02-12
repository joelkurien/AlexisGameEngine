using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisUI.ContentHandling.Interfaces
{
    public interface ProjectValidationInterface
    {
        public Task<bool> ValidateProjectCreation(string fileName, string directoryPath);
    }
}
