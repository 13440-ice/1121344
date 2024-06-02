using Microsoft.VisualBasic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TodoListApp
{
    public partial class Form1 : Form
    {
        private TodoList todoList;
        private string filePath = "todolist.json";

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public object DueDate { get; private set; }
        public int Status { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string itemName = listBox1.SelectedItem.ToString();
                MessageBox.Show(itemName + "被選了");
            }
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                tbTitle.Text = item.Title;
                tbDesc.Text = item.Description;
                tbCreatedDate.Text = item.CreatedDate.ToString();
                dtPickerDue.Value = item.DueDate;
                rbStatus0.Checked = true;
                rbStatus1.Checked = item.Status == 1;
                rbStatus2.Checked = item.Status == 2;
                listBox1.ClearSelected();
                btnAdd.Enabled = false;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            todoList = new TodoList();
            todoList.LoadFromFile(filePath);
            listBox1.DataSource = todoList.GetItems();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            todoList.SaveToFile(filePath);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = tbTitle.Text;
            string description = tbDesc.Text;
            DateTime now = DateTime.Now;
            DateTime dateTime = dtPickerDue.Value;
            TodoItem newitem = new TodoItem();
            {
                Title = title,
                Description = description,
                CreatedDate = now,
                DueDate = dueDate,
                Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0)
            };
            todoList.AddItem(newitem);
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                item.Title = tbTitle.Text;
                item.Description = tbDesc.Text;
                item.DueDate = dtPickerDue.Value;
                item.Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0);
                listBox1.DataSource = null;
                listBox1.DataSource = todoList.GetItems();
                MessageBox.Show("修改成功");
                resetUI();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                DialogResult confirm = MessageBox.Show("確定刪除?",MessageBoxButton.YesNo);
                if(confirm == DialogResult.Yes)
                {
                    todoList.RemoveItem(item);
                    MessageBox.Show("刪除成功");
                    resetUI();
                }
            }   
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private void resetUI()
        {
            tbTitle.Text = " ";
            tbDesc.Text = " ";
            tbCreatedDate.Text = " ";
            dtPickerDue.Value = DateTime.Today;
            rbStatus0.Checked = true;
            listBox1.ClearSelected();
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
