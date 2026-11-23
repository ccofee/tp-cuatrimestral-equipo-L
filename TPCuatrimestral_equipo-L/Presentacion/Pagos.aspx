<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="Presentacion.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Historial de Pagos</h2>
    <hr />

    <div class="row mb-4">
        <div class="col-md-6 mb-2">
            <div class="card text-center">
                <div class="card-body">
                    <h6 class="card-title text-muted">Cantidad de Cobros</h6>
                    <h3>
                        <asp:Literal ID="litCantidadPagos" runat="server" Text="0"></asp:Literal></h3>
                </div>
            </div>
        </div>
        <div class="col-md-6 mb-2">
            <div class="card text-center">
                <div class="card-body">
                    <h6 class="card-title text-muted">Total Recaudado</h6>
                    <h3>
                        <asp:Literal ID="litTotalRecaudado" runat="server" Text="$ 0,00"></asp:Literal></h3>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4 mb-3">
            <label for="txtFiltro" class="form-label">Buscar por DNI o alumno</label>
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control"
                AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged"></asp:TextBox>
        </div>
        <div class="col-md-3 mb-3">
            <label for="ddlPeriodo" class="form-label">Período</label>
            <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="form-select"
                AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                <asp:ListItem Text="Todos" Value="Todos" />
                <asp:ListItem Text="Hoy" Value="Hoy" />
                <asp:ListItem Text="Últimos 7 días" Value="7Dias" />
                <asp:ListItem Text="Este mes" Value="MesActual" />
            </asp:DropDownList>
        </div>
        <div class="col-md-3 mb-3">
            <label for="ddlPlan" class="form-label">Plan</label>
            <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-select"
                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                <asp:ListItem Text="Todos" Value="0" />
            </asp:DropDownList>
        </div>
        <div class="col-md-2 mb-3 d-flex align-items-end">
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"
                CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click" />
        </div>
    </div>

    <asp:GridView ID="dgvPagos" runat="server"
        CssClass="table table-striped table-hover"
        AutoGenerateColumns="False"
        DataKeyNames="Id"
        EmptyDataText="No hay pagos registrados para los filtros seleccionados.">
        <Columns>
            <asp:BoundField DataField="FechaPago" HeaderText="Fecha de Pago" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="AlumnoDNI" HeaderText="DNI" />
            <asp:BoundField DataField="AlumnoNombre" HeaderText="Alumno" />
            <asp:BoundField DataField="PlanDescripcion" HeaderText="Plan" />
            <asp:BoundField DataField="Monto" HeaderText="Importe" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
</asp:Content>
