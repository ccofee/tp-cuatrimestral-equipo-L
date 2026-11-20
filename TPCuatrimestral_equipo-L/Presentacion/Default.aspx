<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>GymConnect</h1>
    <p class="lead">Sistema de gestion de planes, alumnos, horarios y reservas.</p>

    <asp:PlaceHolder ID="phAdmin" runat="server" Visible="false">
        <h2 class="mt-4">Panel de Gestión</h2>

        <div class="row g-3 mb-4">
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h6 class="card-subtitle text-muted mb-2">Socios Activos</h6>
                        <p class="display-6 mb-0"><asp:Literal ID="litSociosActivos" runat="server" /></p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h6 class="card-subtitle text-muted mb-2">Cuotas al Día</h6>
                        <p class="display-6 mb-0 text-success"><asp:Literal ID="litCuotasAlDia" runat="server" /></p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card text-center">
                    <div class="card-body">
                        <h6 class="card-subtitle text-muted mb-2">Atrasadas</h6>
                        <p class="display-6 mb-0 text-danger"><asp:Literal ID="litCuotasAtrasadas" runat="server" /></p>
                    </div>
                </div>
            </div>
        </div>

        <div class="mb-3" style="max-width: 320px;">
            <label for="ddlVista" class="form-label">Vista</label>
            <asp:DropDownList ID="ddlVista" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlVista_SelectedIndexChanged">
                <asp:ListItem Text="Cuotas atrasadas" Value="atrasadas" />
                <asp:ListItem Text="Próximos a vencer (5 días)" Value="proximos" />
                <asp:ListItem Text="Fidelización (6+ meses)" Value="fidelizacion" />
            </asp:DropDownList>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="dgvDashboard" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-hover align-middle" GridLines="None"
                EmptyDataText="No hay socios para esta vista.">
                <HeaderStyle CssClass="table-dark" />
                <Columns>
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Plan.Descripcion" HeaderText="Plan" />
                    <asp:BoundField DataField="FechaVencimientoPlan" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Antiguedad" HeaderText="Antigüedad" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phAlumno" runat="server" Visible="false">
        <div class="card mt-4" style="max-width: 480px;">
            <div class="card-body">
                <h4 class="card-title">Hola, <asp:Literal ID="litNombreAlumno" runat="server" />!</h4>
                <asp:PlaceHolder ID="phDatosAlumno" runat="server" Visible="false">
                    <p class="mb-1"><strong>Plan:</strong> <asp:Literal ID="litPlanAlumno" runat="server" /></p>
                    <p class="mb-1"><strong>Vencimiento:</strong> <asp:Literal ID="litVencimientoAlumno" runat="server" /></p>
                    <p class="mb-3"><asp:Literal ID="litEstadoAlumno" runat="server" /></p>
                    <a href="Reservas.aspx" class="btn btn-primary">Reservar Clase</a>
                </asp:PlaceHolder>
                <asp:Label ID="lblSinFicha" runat="server" CssClass="text-muted" Visible="false"
                    Text="Todavía no tenés una ficha de alumno vinculada. Hablá con recepción."></asp:Label>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
