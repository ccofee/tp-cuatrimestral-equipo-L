<%@ Page Title="Cambiar contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="Presentacion.CambiarClave" %>

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
                <h2 class="text-center mb-3">Elegí tu contraseña</h2>
                <p class="text-muted text-center small mb-4">Por seguridad, tenés que definir una contraseña propia antes de continuar.</p>

                <div class="mb-3">
                    <label for="txtNuevaClave" class="form-label">Nueva contraseña</label>
                    <asp:TextBox ID="txtNuevaClave" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtNuevaClave" ErrorMessage="La contraseña es requerida" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="txtConfirmarClave" class="form-label">Confirmar contraseña</label>
                    <asp:TextBox ID="txtConfirmarClave" runat="server" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtConfirmarClave" ErrorMessage="Confirmá la contraseña" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server"
                        ControlToValidate="txtConfirmarClave" ControlToCompare="txtNuevaClave" ErrorMessage="Las contraseñas no coinciden" CssClass="validacion" Display="Dynamic"></asp:CompareValidator>
                </div>

                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary w-100" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Label ID="lblError" runat="server" CssClass="validacion d-block mt-2 text-center" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
