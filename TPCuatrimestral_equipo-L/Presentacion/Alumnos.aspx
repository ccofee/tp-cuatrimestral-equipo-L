<%@ Page Title="Alumnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alumnos.aspx.cs" Inherits="Presentacion.Alumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-12">
            <h2>Gestión de Alumnos</h2>
            <hr />
        </div>
    </div>

    <div class="row">

        <style>
            .validacion {
                color: red;
                font-size: 12px;
            }
        </style>

        <div class="col-md-5">
            <!-- DNI obligatorio y 7 u 8 digitos -->
            <div class="mb-3">
                <label for="txtDni" class="form-label">DNI</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="txtDni" ErrorMessage="El DNI es requerido" CssClass="validacion"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server"
                    ControlToValidate="txtDni" ErrorMessage="Solo números" ValidationExpression="^[0-9]+$" CssClass="validacion"></asp:RegularExpressionValidator>
                <asp:Label ID="lblErrorDni" runat="server" CssClass="validacion" Visible="false"></asp:Label>
            </div>

            <div class="row">
                <!-- nombre obligatorio -->
                <div class="col-md-6 mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido" CssClass="validacion"></asp:RequiredFieldValidator>
                </div>
                <!-- apellido obligatorio -->
                <div class="col-md-6 mb-3">
                    <label for="txtApellido" class="form-label">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es requerido" CssClass="validacion"></asp:RequiredFieldValidator>
                </div>
            </div>
            <!-- email con validator -->
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato email, por favor"
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" CssClass="validacion"></asp:RegularExpressionValidator>
            </div>
            <!-- telefono -->
            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <!-- fecha nac -->
            <div class="mb-3">
                <label for="txtFechaNacimiento" class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <!-- plan obligatorio -->
            <div class="mb-3">
                <label for="ddlPlan" class="form-label">Plan</label>
                <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                    <asp:ListItem Text="-- Seleccione un Plan --" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server"
                    ControlToValidate="ddlPlan" InitialValue="0" ErrorMessage="Debe seleccionar un plan" CssClass="validacion"></asp:RequiredFieldValidator>
            </div>
            <!-- fecha vencimiento -->
            <div class="mb-3">
                <label for="txtFechaVencimiento" class="form-label">Vencimiento del Plan</label>
                <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <!-- botones con CausesValidation -->
            <div class="mb-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnRenovar" runat="server" Text="Renovar Plan" CssClass="btn btn-success" CausesValidation="false" OnClick="btnRenovar_Click" />
            </div>
        </div>

        <div class="col-md-7">
            <h4>Listado</h4>

            <div class="mb-3">
                <label for="txtFiltro" class="form-label">Filtrar</label>
                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
            </div>

            <asp:GridView ID="dgvAlumnos" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False"
                AutoGenerateSelectButton="True"
                DataKeyNames="Id"
                OnSelectedIndexChanged="dgvAlumnos_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Plan.Descripcion" HeaderText="Plan" />
                    <asp:BoundField DataField="FechaVencimientoPlan" HeaderText="Vencimiento del Plan" DataFormatString="{0:dd/MM/yyyy}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
