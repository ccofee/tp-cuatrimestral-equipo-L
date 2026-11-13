<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Presentacion.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Usuarios</h2>

    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }
    </style>

    <div class="card mb-4">
        <div class="card-header">Nuevo usuario</div>
        <div class="card-body">
            <div class="row g-3 align-items-end">
                <div class="col-md-4">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Email" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="El email es requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" CssClass="validacion" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblClave" runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Contraseña" />
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" MaxLength="100" TextMode="Password" />
                    <small class="text-muted">Vacío para no cambiarla</small>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblRol" runat="server" AssociatedControlID="ddlRol" CssClass="form-label" Text="Rol" />
                    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Administrador" Value="1" />
                        <asp:ListItem Text="Alumno" Value="2" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row g-3 align-items-end mt-2">
                <div class="col-md-4">
                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" CssClass="form-label" Text="Nombre" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblApellido" runat="server" AssociatedControlID="txtApellido" CssClass="form-label" Text="Apellido" />
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtApellido" ErrorMessage="El apellido es requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4 d-grid gap-2 d-md-flex">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" CausesValidation="false" OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" CausesValidation="false" OnClick="btnEliminar_Click" />
                </div>
            </div>
            <asp:Label ID="lblErrorUsuario" runat="server" CssClass="validacion" Visible="false"></asp:Label>
        </div>
    </div>

    <asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="false" AutoGenerateSelectButton="true" DataKeyNames="Id"
        CssClass="table table-striped table-hover align-middle" GridLines="None"
        EmptyDataText="No hay usuarios activos." OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged">
        <HeaderStyle CssClass="table-dark" />
        <Columns>
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="RolDescripcion" HeaderText="Rol" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        </Columns>
    </asp:GridView>
</asp:Content>
