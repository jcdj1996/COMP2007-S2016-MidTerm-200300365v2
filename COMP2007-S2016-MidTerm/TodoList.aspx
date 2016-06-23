<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm.TodoList1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add New</a>
    <div class="container">
                <div>
                    <label for="PageSizeDropDownList">Records per Page: </label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>
    <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="ToDoGridView" AutoGenerateColumns="false" DataKeyNames="ToDoID"
                    OnRowDeleting="ToDoGridView_RowDeleting" AllowPaging="true" PageSize="3"
                    OnPageIndexChanging="ToDoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="ToDoGridView_Sorting" OnRowDataBound="ToDoGridView_RowDataBound" 
                    PagerStyle-CssClass="pagination-ys">
        <Columns>
                        <asp:BoundField DataField="ToDoID" HeaderText="ToDo ID" Visible="false" SortExpression="ToDoID" />
                        <asp:BoundField DataField="ToDoName" HeaderText="Task Name" Visible="true" SortExpression="ToDoName" />
                        <asp:BoundField DataField="ToDoNotes" HeaderText="Notes" Visible="true" SortExpression="ToDoNotes" />
                        <asp:BoundField DataField="Completed" HeaderText="Completed" Visible="true" />
                        
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="/ToDoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="ToDoID" DataNavigateUrlFormatString="ToDoDetails.aspx?ToDoID={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns> 
    </asp:GridView>
        </div>
</asp:Content>
