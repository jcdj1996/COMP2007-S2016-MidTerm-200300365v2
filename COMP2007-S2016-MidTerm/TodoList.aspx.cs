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
    public partial class TodoList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the ToDo grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "ToDoID"; // default sort column
                Session["SortDirection"] = "ASC";
                // Get the ToDo data
                this.GetToDo();
            }
        }
        protected void GetToDo()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // query the ToDo Table using EF and LINQ
                var ToDoItem = (from allToDos in db.Todos
                                select allToDos);

                // bind the result to the GridView
                ToDoGridView.DataSource = ToDoItem.AsQueryable().OrderBy(SortString).ToList();
                ToDoGridView.DataBind();

            }
        }
        protected void ToDoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected ToDoID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(ToDoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected ToDo in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the ToDo class and store the query string inside of it
                Todo deletedToDo = (from TodoRecords in db.Todos
                                    where TodoRecords.TodoID == TodoID
                                    select TodoRecords).FirstOrDefault();

                // remove the selected ToDo from the db
                db.Todos.Remove(deletedToDo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetToDo();
            }
        }
        protected void ToDoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            ToDoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetToDo();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            ToDoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetToDo();
        }

        protected void ToDoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetToDo();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void ToDoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < ToDoGridView.Columns.Count - 1; index++)
                    {
                        if (ToDoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}