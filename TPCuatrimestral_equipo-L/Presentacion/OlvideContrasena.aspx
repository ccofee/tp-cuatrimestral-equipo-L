<%@ Page Title="Olvidé mi contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OlvideContrasena.aspx.cs" Inherits="Presentacion.OlvideContrasena" %>

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
                <h2 class="text-center mb-3">Olvidé mi contraseña</h2>
                <p class="text-muted text-center small mb-4">Ingresá tu email y te mandamos una contraseña provisoria.</p>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="El email es requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>

                <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-primary w-100" Text="Enviar" OnClick="btnEnviar_Click" />

                <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-3 text-center text-success" Visible="false"></asp:Label>

                <div class="text-center mt-3">
                    <a href="Login.aspx">Volver a Iniciar sesión</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
