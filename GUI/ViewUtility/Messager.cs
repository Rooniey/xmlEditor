using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GUI.ViewUtility
{
    public static class Messager
    {
        public static void DisplayError(string errorMessage)
        {
            string caption = "Error";
            MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void DisplayValidationErrors(List<string> errors)
        {
            string errorsMessage = errors.Aggregate((a, b) => a + "\n" + b);
            string message = $"Provided xml file does not meet the requirements of XML Schema\n {errorsMessage}";
            string caption = "Validation failed";
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
