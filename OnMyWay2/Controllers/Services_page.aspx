<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/Site1.Master" AutoEventWireup="true" CodeBehind="Services_page.aspx.cs" Inherits="OnMyWay2.Controllers.Services_page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	 <form id="form1" runat="server">

          <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false"
              BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
          
              
              DataKeyNames="service_id">
                  <Columns>
                      
                     <asp:BoundField DataField="service_id" HeaderText ="service_id" />
                    <asp:BoundField DataField="service_date" HeaderText ="service_date" />
                    <asp:BoundField DataField="user_name" HeaderText ="user_name" />
                         <asp:BoundField DataField="user_phone" HeaderText ="user_phone" />
                         <asp:BoundField DataField="user_id" HeaderText ="user_id" />
					      <asp:BoundField DataField="service_pickup" HeaderText ="Pick Up location" />
                         <asp:BoundField DataField="service_destination" HeaderText ="Destination" />
                         <asp:BoundField DataField="service_gender" HeaderText =" gender" />
                         <asp:BoundField DataField="service_space" HeaderText ="Space" />
					     <asp:BoundField DataField="service_status" HeaderText ="Status" />
                         <asp:BoundField DataField="service_type" HeaderText ="Type 1 (seeked ) 2 ( Provided )" />


	
                  </Columns>
        </asp:GridView>
		       </form>
</asp:Content>
