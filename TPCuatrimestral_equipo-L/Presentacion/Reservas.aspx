<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Presentacion.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <h2 class="mb-4">Reservas de la semana</h2>

        <div class="card mb-4">
            <div class="card-body">

                <div class="row mb-3">
                <asp:PlaceHolder ID="phAlumnoAdmin" runat="server" Visible="false">
                    <div class="col-md-3">
                        <label>Alumno</label>
                        <asp:DropDownList
                    ID="ddlAlumno"
                    runat="server"
                    CssClass="form-select">
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
                        <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select">
                            <asp:ListItem Text="-- Seleccione un horario --" Value="0" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label>Fecha de clase</label>
                        <asp:TextBox
                                ID="txtFechaClase"
                                  runat="server"
                                CssClass="form-control"
                                TextMode="Date" />
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
                    OnClick="btnCancelarReserva_Click"/>

                <asp:Label
                    ID="lblMensajeReserva"
                    runat="server"
                    CssClass="d-block mt-3"
                    Visible="false">
                </asp:Label>
            </div>
        </div>
                
        <asp:GridView
            ID="dgvReservas"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered"
            DataKeyNames="Id">

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

                <asp:BoundField
                    DataField="Estado"
                    HeaderText="Estado" />

                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            </Columns>
            
            <SelectedRowStyle CssClass="table-primary fw-semibold" />

        </asp:GridView>

    </div>

</asp:Content>