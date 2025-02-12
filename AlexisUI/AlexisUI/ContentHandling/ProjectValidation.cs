using AlexisUI.ContentHandling.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlexisUI.ContentHandling
{
    public class ProjectValidation : ProjectValidationInterface
    {
        public static bool isProjTypeSelected = false;
        public async Task<bool> ValidateProjectCreation(string fileName, string directoryPath)
        {
            var isValidDir = await DirectoryLocationValidation(directoryPath);
            var isProjSelected = await Task.Run(() => ProjectTypeSelectionValidation());
            var isProjNameValid = await Task.Run(() => ProjectNameValidation(fileName));
            return isValidDir && isProjSelected && isProjNameValid;
        }
        private Task<bool> DirectoryLocationValidation(string directoryPath)
        {
            return Task.Run(() => Directory.Exists(directoryPath));
        }

        private bool ProjectTypeSelectionValidation()
        {
            return isProjTypeSelected;
        }

        private bool ProjectNameValidation(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || Regex.IsMatch(fileName, @"[@#$%^&]"))
                return false;

            HashSet<string> reservedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4",
                "LPT1", "LPT2", "LPT3", "NULL", "VOID", "CLASS", "STRUCT",
                "INT", "FLOAT", "DOUBLE", "PUBLIC", "PRIVATE", "STATIC",
                "ABSTRACT", "NAMESPACE", "USING"
            };

            foreach (string word in reservedWords)
            {
                if (fileName.Equals(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
