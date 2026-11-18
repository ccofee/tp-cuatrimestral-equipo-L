<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Presentacion.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-danger">
        <h2>Ocurrió un error inesperado</h2>
        <p class="mb-0">Volvé al inicio e intentá de nuevo.</p>
    </div>
    <asp:Literal ID="litDetalle" runat="server" />
    <a class="btn btn-primary" href="Default.aspx">Volver al inicio</a>
</asp:Content>
