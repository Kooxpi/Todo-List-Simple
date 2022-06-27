using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class TodoList : Form
    {
        public TodoList()
        {
            InitializeComponent();
        }
        String filePath = @"todo.txt";
        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(filePath))
                TodoListBox.Items.AddRange(System.IO.File.ReadAllLines(filePath));
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            TodoListBox.Items.Clear();
            File.AppendAllText(@"todo.txt", inputBox.Text + Environment.NewLine);
            inputBox.Text = "";
            TodoListBox.Items.AddRange(System.IO.File.ReadAllLines(filePath));
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(TodoListBox.Text, "");
            File.WriteAllText(filePath, text);
            File.WriteAllLines(filePath, File.ReadAllLines(filePath).Where(l => !string.IsNullOrWhiteSpace(l)));
            TodoListBox.Items.Clear();
            TodoListBox.Items.AddRange(System.IO.File.ReadAllLines(filePath));
            inputBox.Text = "";
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(filePath);
            text = text.Replace(TodoListBox.Text, inputBox.Text);
            if(inputBox.Text.Trim() == "")
            {
                MessageBox.Show("Entry cannot be blank!");
            }else { 
                File.WriteAllText(filePath, text);
                TodoListBox.Items.Clear();
                TodoListBox.Items.AddRange(System.IO.File.ReadAllLines(filePath));
                inputBox.Text = "";
            }
        }

        private void TodoListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            int sCount = TodoListBox.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked) { ++sCount; }
            if (e.NewValue == CheckState.Unchecked) { --sCount; }
            //MessageBox.Show(sCount.ToString());
            if (sCount != 1) { inputBox.Text = ""; } else { inputBox.Text = TodoListBox.Text; }
        }
    }


}
