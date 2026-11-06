<%@ Page Title="Planes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="Presentacion.Planes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Planes</h2>

    <div class="card mb-4">
        <div class="card-header">Nuevo plan</div>
        <div class="card-body">
            <div class="row g-3 align-items-end">
                <div class="col-md-5">
                    <asp:Label ID="lblDescripcion" runat="server" AssociatedControlID="txtDescripcion" CssClass="form-label" Text="Descripcion" />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" />
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblPrecio" runat="server" AssociatedControlID="txtPrecio" CssClass="form-label" Text="Precio mensual" />
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="any" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblClasesSemanales" runat="server" AssociatedControlID="txtClasesSemanales" CssClass="form-label" Text="Clases por semana" />
                    <asp:TextBox ID="txtClasesSemanales" runat="server" CssClass="form-control" TextMode="Number" />
                </div>
                <div class="col-md-2 d-grid">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>

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
