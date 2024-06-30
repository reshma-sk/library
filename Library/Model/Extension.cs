using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library.Model
{
    public static class Extension
    {
        public static void StartAndSavePosition(this Form currentForm, Form newForm)
        {
            newForm.Closed += (s, args) => currentForm.Close();
            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = currentForm.Location;
            currentForm.Hide();
            newForm.Show();
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}