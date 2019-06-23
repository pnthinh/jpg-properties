using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace WF
{
    public partial class Form1 : Form
    {
        string folderpath = @"C:\Image";
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add(@"https://myteashirts.com/");
            comboBox1.Items.Add(@"https://pdtshirt.com/");
            comboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //MessageBox.Show("You selected: " + dialog.FileName);
                folderpath = dialog.FileName;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            doIt();
        }
        void doIt()
        {
            DirectoryInfo d = new DirectoryInfo(folderpath);

            foreach (var file in d.GetFiles("*.jpg"))
            {
                string filePath = file.FullName;
                string name = Path.GetFileNameWithoutExtension(filePath);
                var img = ShellFile.FromFilePath(filePath);
                try
                {
                    ShellPropertyWriter propertyWriter = img.Properties.GetPropertyWriter();
                    propertyWriter.WriteProperty(SystemProperties.System.Title, name);
                    propertyWriter.WriteProperty(SystemProperties.System.Subject, name);
                    propertyWriter.WriteProperty(SystemProperties.System.Rating, 99);
                    propertyWriter.WriteProperty(SystemProperties.System.Comment, "I like " + name);
                    propertyWriter.WriteProperty(SystemProperties.System.Title, name);
                    propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { comboBox1.Text });
                    propertyWriter.WriteProperty(SystemProperties.System.Copyright, comboBox1.Text);
                    propertyWriter.Close();
                    // Read and Write:
                    /*
                    img.Properties.System.Title.Value = file.Name;
                    img.Properties.System.Subject.Value = file.Name;
                    img.Properties.System.Rating.Value = 99; // 5 stars
                    img.Properties.System.Comment.Value = "I like " + file.Name;
                    img.Properties.System.Author.Value = new string[] { comboBox1.Text };
                    img.Properties.System.Copyright.Value = comboBox1.Text; */
                }
                catch(Exception e)
                {
                    //MessageBox.Show("Error !!!");
                    continue;
                }                
            }
        }
    }
}

/*
 TAI LIEU THAM KHAO: 
 *https://stackoverflow.com/questions/11861151/find-all-files-in-a-folder
string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
DirectoryInfo d = new DirectoryInfo(filepath);

foreach (var file in d.GetFiles("*.txt"))
{
      Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);
}

 *https://stackoverflow.com/questions/5337683/how-to-set-extended-file-properties# 
 using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

string filePath = @"C:\temp\example.docx";
var file = ShellFile.FromFilePath(filePath);

// Read and Write:

string[] oldAuthors = file.Properties.System.Author.Value;
string oldTitle = file.Properties.System.Title.Value;

file.Properties.System.Author.Value = new string[] { "Author #1", "Author #2" };
file.Properties.System.Title.Value = "Example Title";

// Alternate way to Write:

ShellPropertyWriter propertyWriter =  file.Properties.GetPropertyWriter();
propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { "Author" });
propertyWriter.Close();
*/
