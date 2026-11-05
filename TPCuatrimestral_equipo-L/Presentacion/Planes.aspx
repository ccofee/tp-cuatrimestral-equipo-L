<%@ Page Title="Planes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="Presentacion.Planes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Planes</h2>

    <asp:GridView ID="dgvPlanes" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
        CssClass="table table-striped table-hover align-middle" GridLines="None"
        EmptyDataText="No hay planes activos.">
        <HeaderStyle CssClass="table-dark" />
        <Columns>
            <asp:BoundField DataField="Descripcion" HeaderText="Plan" />
            <asp:BoundField DataField="Precio" HeaderText="Precio mensual" DataFormatString="{0:C}"
                HeaderStyle-CssClass="text-end" ItemStyle-CssClass="text-end" />
            <asp:BoundField DataField="ClasesSemanales" HeaderText="Clases por semana"
                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        </Columns>
    </asp:GridView>
</asp:Content>
