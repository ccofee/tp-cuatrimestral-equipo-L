<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Presentacion.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }
    </style>

    <h2 class="mb-4">Mi Perfil</h2>

    <div class="card mb-4" style="max-width: 480px;">
        <div class="card-body">
            <h5 class="card-title">Mis datos</h5>
            <p class="mb-1"><strong>Nombre:</strong> <asp:Literal ID="litNombre" runat="server" /></p>
            <p class="mb-1"><strong>Email:</strong> <asp:Literal ID="litEmail" runat="server" /></p>
            <p class="mb-1"><strong>Rol:</strong> <asp:Literal ID="litRol" runat="server" /></p>

            <asp:PlaceHolder ID="phDatosAlumno" runat="server" Visible="false">
                <hr />
                <p class="mb-1"><strong>DNI:</strong> <asp:Literal ID="litDni" runat="server" /></p>
                <p class="mb-1"><strong>Plan:</strong> <asp:Literal ID="litPlan" runat="server" /></p>
                <p class="mb-1"><strong>Vencimiento:</strong> <asp:Literal ID="litVencimiento" runat="server" /></p>
                <p class="mb-0"><asp:Literal ID="litEstado" runat="server" /></p>
            </asp:PlaceHolder>
        </div>
    </div>

    <div class="card" style="max-width: 480px;">
        <div class="card-body">
            <h5 class="card-title">Cambiar contraseña</h5>
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
            <asp:Button ID="btnCambiarClave" runat="server" CssClass="btn btn-primary" Text="Actualizar contraseña" OnClick="btnCambiarClave_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mt-2" Visible="false"></asp:Label>
        </div>
    </div>
</asp:Content>
