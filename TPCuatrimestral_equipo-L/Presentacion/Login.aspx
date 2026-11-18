<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }
    </style>

    <div class="d-flex justify-content-center align-items-center" style="min-height: 70vh;">
        <div class="card shadow-sm" style="width: 100%; max-width: 380px;">
            <div class="card-body p-4">
                <h2 class="text-center mb-4">Iniciar sesión</h2>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="El email es requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="txtClave" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtClave" ErrorMessage="La contraseña es requerida" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-primary w-100" Text="Ingresar" OnClick="btnIngresar_Click" />
                <asp:Label ID="lblErrorLogin" runat="server" CssClass="validacion d-block mt-2 text-center" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
