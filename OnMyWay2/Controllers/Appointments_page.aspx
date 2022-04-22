<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/Site1.Master" AutoEventWireup="true" CodeBehind="Appointments_page.aspx.cs" Inherits="OnMyWay2.Controllers.Appointments_page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	 <form id="form1" runat="server">

          <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false"
              BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
          
              
              DataKeyNames="appointment_id">
                  <Columns>
                      
                     <asp:BoundField DataField="appointment_id" HeaderText ="appointment_id" />
                    <asp:BoundField DataField="customer_name" HeaderText ="customer_name" />
                    <asp:BoundField DataField="customer_phone" HeaderText ="customer_phone" />
                         <asp:BoundField DataField="customer_gender" HeaderText ="customer_gender" />
                         <asp:BoundField DataField="provider_name" HeaderText ="provider_name" />
					      <asp:BoundField DataField="provider_phone" HeaderText ="provider_phone" />

                         <asp:BoundField DataField="provider_gender" HeaderText ="provider_gender" />
                         <asp:BoundField DataField="pickup_location" HeaderText =" pickup_location" />
                         <asp:BoundField DataField="destination" HeaderText ="destination" />

					     <asp:BoundField DataField="date" HeaderText ="date" />
                         <asp:BoundField DataField="space" HeaderText ="space" />

					   <asp:BoundField DataField="customer_id" HeaderText ="customer_id" />
                         <asp:BoundField DataField="provider_id" HeaderText ="provider_id" />
					   <asp:BoundField DataField="service_id" HeaderText ="service_id" />

	

	
	
	
                  </Columns>
        </asp:GridView>
		       </form>
</asp:Content>
