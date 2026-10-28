<%@ Page Title="Planes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="Presentacion.Planes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Planes</h2>
    <asp:GridView ID="dgvPlanes" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="true">
    </asp:GridView>
</asp:Content>
