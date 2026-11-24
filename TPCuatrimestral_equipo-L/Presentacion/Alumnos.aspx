<%@ Page Title="Alumnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alumnos.aspx.cs" Inherits="Presentacion.Alumnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }

        .table-responsive table th,
        .table-responsive table td {
            white-space: nowrap;
        }

        .chk-atrasado input[type="checkbox"] {
            -webkit-appearance: none;
            appearance: none;
            width: 17px;
            height: 17px;
            border: 2px solid #6c757d;
            border-radius: 3px;
            background-color: #ffffff;
            vertical-align: middle;
            position: relative;
        }

            .chk-atrasado input[type="checkbox"]:checked {
                background-color: #dc3545;
                border-color: #dc3545;
            }

                .chk-atrasado input[type="checkbox"]:checked::after {
                    content: "✓";
                    color: #ffffff;
                    font-size: 13px;
                    font-weight: bold;
                    position: absolute;
                    top: -3px;
                    left: 2px;
                }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-12">
            <h2>Gestión de Alumnos</h2>
            <hr />
        </div>
    </div>

    <div class="row">

        <div class="col-md-4">
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
            <div class="row">
                <!-- telefono -->
                <div class="col-md-6 mb-3">
                    <label for="txtTelefono" class="form-label">Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <!-- fecha nac -->
                <div class="col-md-6 mb-3">
                    <label for="txtFechaNacimiento" class="form-label">Fecha de Nacimiento</label>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <!-- plan obligatorio -->
                <div class="col-md-6 mb-3">
                    <label for="ddlPlan" class="form-label">Plan</label>
                    <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Seleccione un Plan --" Value="0" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="ddlPlan" InitialValue="0" ErrorMessage="Debe seleccionar un plan" CssClass="validacion"></asp:RequiredFieldValidator>
                </div>
                <!-- fecha vencimiento -->
                <div class="col-md-6 mb-3">
                    <label for="txtFechaVencimiento" class="form-label">Vencimiento del Plan</label>
                    <asp:TextBox ID="txtFechaVencimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <!-- botones con CausesValidation -->
            <div class="mb-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnRenovar" runat="server" Text="Registrar Pago" CssClass="btn btn-success" CausesValidation="false" OnClick="btnRenovar_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success d-block mt-3" Visible="false"></asp:Label>
            </div>
        </div>

        <div class="col-md-8">
            <h4>Listado</h4>

            <!-- filtros -->
            <div class="row">
                <div class="col-md-8 mb-3">
                    <label for="txtFiltro" class="form-label">Filtrar</label>
                    <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                    <label for="ddlEstado" class="form-label">Estado de cuota</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="Todos" />
                        <asp:ListItem Text="Al día" Value="AlDia" />
                        <asp:ListItem Text="Atrasados" Value="Atrasado" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="dgvAlumnos" runat="server" CssClass="table table-striped table-hover"
                    AutoGenerateColumns="False"
                    AutoGenerateSelectButton="True"
                    DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvAlumnos_SelectedIndexChanged"
                    AllowPaging="true"
                    PageSize="12"
                    OnPageIndexChanging="dgvAlumnos_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="DNI" HeaderText="DNI" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Alumno" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Plan" HeaderText="Plan" />
                        <asp:BoundField DataField="FechaVencimientoPlan" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Antiguedad" HeaderText="Antigüedad" />
                        <asp:CheckBoxField DataField="Atrasado" HeaderText="Atrasado" ItemStyle-CssClass="chk-atrasado text-center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
