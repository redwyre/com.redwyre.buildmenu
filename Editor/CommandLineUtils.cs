using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

#nullable enable

namespace BuildMenu.Editor
{
    public static class CommandLineUtils
    {
        /// <summary>
        /// Check if a command line parameter has been set.
        /// </summary>
        /// <param name="parameter">The parameter, without and starting "-".</param>
        /// <returns>true if the parameter is present, otherwise null.</returns>
        public static bool? GetCommandLineParameter(string parameter)
        {
            var env = Environment.GetCommandLineArgs();

            var fullParameter = "-" + parameter;

            var index = Array.FindIndex(env, x => x.Equals(fullParameter, StringComparison.InvariantCultureIgnoreCase));

            return (index != -1) ? true : null;
        }
    }
}
