using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//custom imports
using System.Linq.Dynamic;
using COMP2007_S2016_MidTerm.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetToDo();
            }
        }

        protected void GetToDo()
        {
            // populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            using (TodoConnection db = new TodoConnection())
            {
                Todo updatedTodo = (from Todo in db.Todos
                                    where Todo.TodoID == TodoID
                                    select Todo).FirstOrDefault();


                if (updatedTodo != null)
                {
                    TaskNameTextBox.Text = updatedTodo.TodoName;
                    NotesTextBox.Text = updatedTodo.TodoNotes;
                    CompleteCheck.Checked = updatedTodo.Completed.Value;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    newTodo = (from Todo in db.Todos
                               where Todo.TodoID == TodoID
                               select Todo).FirstOrDefault();
                }

                // add form data to the new todo
                newTodo.TodoName = TaskNameTextBox.Text;
                newTodo.TodoNotes = NotesTextBox.Text;
                newTodo.Completed = CompleteCheck.Checked;


                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated students page
                Response.Redirect("TodoList.aspx");
            }
        }
    }

}