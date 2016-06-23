<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm.TodoList1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="ToDoGridView" AutoGenerateColumns="false" DataKeyNames="ToDoID"
                    OnRowDeleting="ToDoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                    OnPageIndexChanging="ToDoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="ToDoGridView_Sorting" OnRowDataBound="ToDoGridView_RowDataBound" 
                    PagerStyle-CssClass="pagination-ys">
        <Columns>
                        <asp:BoundField DataField="ToDoID" HeaderText="ToDo ID" Visible="true" SortExpression="ToDoID" />
                        <asp:BoundField DataField="ToDoName" HeaderText="Task Name" Visible="true" SortExpression="ToDoName" />
                        <asp:BoundField DataField="ToDoNotes" HeaderText="Notes" Visible="true" SortExpression="ToDoNotes" />
                        
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="/ToDoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="ToDoID" DataNavigateUrlFormatString="ToDoDetails.aspx?ToDoID={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns> 
    </asp:GridView>
</asp:Content>
