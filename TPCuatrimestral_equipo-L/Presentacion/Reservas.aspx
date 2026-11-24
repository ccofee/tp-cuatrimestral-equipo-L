<%@ Page Title="Reservas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Presentacion.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <h2 class="mb-4"><asp:Literal ID="litTitulo" runat="server" /></h2>

        <div class="card mb-4">
            <div class="card-body">
             <h5 class="card-title mb-3">Nueva reserva</h5>
                <div class="row mb-3">
                <asp:PlaceHolder ID="phAlumnoAdmin" runat="server" Visible="false">
                    <div class="col-md-3">
                        <label>Alumno</label>
                        <asp:DropDownList
                            ID="ddlAlumno"
                            runat="server"
                            CssClass="form-select"
                            AppendDataBoundItems="true"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAlumno_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccione un alumno --" Value="0" />
                        </asp:DropDownList>
                    </div>
                    </asp:PlaceHolder>
                    <div class="col-md-3">
                            <label>Actividad</label>

                            <asp:DropDownList
                                ID="ddlActividad"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                AppendDataBoundItems="true"
                                OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged">
                                
                                <asp:ListItem Text="-- Seleccione una actividad --" Value="0" />

                            </asp:DropDownList>
                        </div>
                   
                    <div class="col-md-3">
                        <label>Horario</label>
                        <asp:DropDownList
                            ID="ddlHorario"
                            runat="server"
                            CssClass="form-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccione un horario --" Value="0" />
                        </asp:DropDownList>
</div>

                    <div class="col-md-3">
                            <label>Fecha de clase</label>
                            <asp:DropDownList
                                ID="ddlFechaClase"
                                runat="server"
                                CssClass="form-select">
                                <asp:ListItem Text="-- Seleccione una fecha --" Value="" />
                            </asp:DropDownList>
                        </div>

                </div>

                <asp:Button
                    ID="btnReservar"
                    runat="server"
                    Text="Reservar"
                    CssClass="btn btn-primary me-2" 
                    OnClick="btnReservar_Click"/>

                <asp:Button
                    ID="btnCancelarReserva"
                    runat="server"
                    Text="Cancelar reserva"
                    CssClass="btn btn-danger" 
                    OnClick="btnCancelarReserva_Click"
                    OnClientClick="return confirm('¿Estás seguro de cancelar esta reserva?');"/>

                <asp:Label
                    ID="lblMensajeReserva"
                    runat="server"
                    CssClass="d-block mt-3"
                    Visible="false">
                </asp:Label>
            </div>
        </div>
              <h5 class="mb-3">Listado de reservas</h5>
                    <div class="row mb-3">
                <div class="col-md-3">
                    <label>Estado de reserva</label>
                    <asp:DropDownList
                        ID="ddlFiltroEstado"
                        runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="Filtros_SelectedIndexChanged">
                        <asp:ListItem Text="Activas" Value="Activas" />
                        <asp:ListItem Text="Canceladas" Value="Cancelada" />
                        <asp:ListItem Text="Todas" Value="Todas" />
                    </asp:DropDownList>
                </div>
         <asp:PlaceHolder ID="phFiltrosAdmin" runat="server" Visible="false">

                <div class="col-md-3">
                    <label>Actividad</label>
                    <asp:DropDownList
                        ID="ddlFiltroActividad"
                        runat="server"
                        CssClass="form-select"
                        AutoPostBack="true"
                        AppendDataBoundItems="true"
                        OnSelectedIndexChanged="Filtros_SelectedIndexChanged">
                        <asp:ListItem Text="Todas" Value="0" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label>Fecha</label>
                    <asp:TextBox
                        ID="txtFiltroFecha"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Date"
                        AutoPostBack="true"
                        OnTextChanged="Filtros_SelectedIndexChanged" />
                </div>

            </asp:PlaceHolder>
                </div>

        <asp:GridView
                        ID="dgvReservas"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover align-middle"
                        DataKeyNames="Id"
                        EmptyDataText="No se encontraron reservas."
                        AllowPaging="true"
                        PageSize="10"
                        OnPageIndexChanging="dgvReservas_PageIndexChanging">

            <Columns>
               <asp:TemplateField HeaderText="Alumno">
                  <ItemTemplate>
                       <%# Eval("Alumno.Nombre") %> <%# Eval("Alumno.Apellido") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actividad">
                    <ItemTemplate>
                        <%# Eval("Horario.Actividad.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField
                    DataField="FechaClase"
                    HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy}" />

                <asp:TemplateField HeaderText="Horario">
                    <ItemTemplate>
                        <%# ((TimeSpan)Eval("Horario.HoraInicio")).ToString(@"hh\:mm") %>
                        a
                        <%# ((TimeSpan)Eval("Horario.HoraFin")).ToString(@"hh\:mm") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span class='badge <%# Eval("Estado").ToString() == "Reservada" ? "bg-success" : Eval("Estado").ToString() == "EnEspera" ? "bg-warning text-dark" : "bg-danger" %>'>
                        <%# Eval("Estado").ToString() == "EnEspera" ? "En espera" : Eval("Estado") %>
                    </span>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField
                    ShowSelectButton="True"
                    SelectText="Seleccionar"
                    ButtonType="Button"
                    ControlStyle-CssClass="btn btn-outline-primary btn-sm"
                    HeaderText="Acción" />
            </Columns>
            
            <SelectedRowStyle CssClass="table-primary fw-semibold" />

        </asp:GridView>

    </div>

</asp:Content>