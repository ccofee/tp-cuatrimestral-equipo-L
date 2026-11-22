<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Actividades.aspx.cs" Inherits="Presentacion.Actividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-12">
            <h2>Gestión de Actividades</h2>
            <hr />
        </div>
    </div>

    <div class="row">
        <!-- alta y modificacion -->
        <div class="col-md-5">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido" CssClass="validacion"></asp:RequiredFieldValidator>
                <asp:Label ID="lblErrorNombre" runat="server" CssClass="validacion" Visible="false"></asp:Label>
            </div>

            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnEliminar_Click" />
                <asp:Label ID="lblErrorActividad" runat="server" CssClass="validacion d-block mt-2" Visible="false"></asp:Label>
            </div>
        </div>
        <!-- grilla y filtro -->
        <div class="col-md-7">
            <h4>Listado</h4>

            <div class="mb-3">
                <label for="txtFiltro" class="form-label">Filtrar</label>
                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
            </div>

            <asp:GridView ID="dgvActividades" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="false"
                AutoGenerateSelectButton="True"
                DataKeyNames="Id"
                OnSelectedIndexChanged="dgvActividades_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>