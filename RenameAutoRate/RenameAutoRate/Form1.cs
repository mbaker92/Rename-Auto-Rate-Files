/* Author: Matthew Baker
   Project: NCDOT

   Purpose: Renames the distress. files to PSI_distress. in the selected folder and subfolders
   Date: 1/13/2017
   Version: 1.0
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace RenameAutoRate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileToChange = "distress.*";
            string pattern = "^" + Regex.Escape(fileToChange).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
            // Required because of the wildcard

            Regex regex;
            regex = new Regex(pattern);


            DialogResult result = folderBrowserDialog1.ShowDialog();
            // Open the folder browser
             
            if (!string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            { 
                string[] folders = System.IO.Directory.GetDirectories(folderBrowserDialog1.SelectedPath);
                // String array of the subfolders in the selected directory

                for(int folderIndex = 0; folderIndex < folders.Length; folderIndex++)
                    // For loop to go through sub folders in the directory
                {
                    string[] files = System.IO.Directory.GetFiles(folders[folderIndex]);
                    // String array of the files in the sub directory

                    for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
                    {
                        // For loop to go through the files in the sub directory

                        string fileName = System.IO.Path.GetFileName(files[fileIndex]);
                        // Get the filename of the current file being checked

                        string filePath = System.IO.Path.GetDirectoryName(files[fileIndex]);
                        filePath = filePath + @"\";
                        // Get the full path of the current directory and add a \ to the end of it

                        if (regex.IsMatch(fileName))
                        {
                            // Checks if the current file is equal to distress.*

                            System.IO.File.Move( files[fileIndex] , filePath + "PSI_" + fileName );
                            // Rename the old file into the new file with PSI_ in front of the old filename
                        }
                    }
                }

                System.Windows.Forms.MessageBox.Show("Files have been Renamed", "Confirmation");
            }
        }
    }
}
