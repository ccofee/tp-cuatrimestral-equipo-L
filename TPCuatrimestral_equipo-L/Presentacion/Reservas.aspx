<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Presentacion.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <h2 class="mb-4">Reservas de la semana</h2>

        <div class="card mb-4">
            <div class="card-body">

                <div class="row mb-3">

                    <div class="col-md-3">
                        <label>Alumno</label>
                        <asp:TextBox ID="txtAlumno" runat="server" CssClass="form-control" />
                    </div>

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
                    CssClass="btn btn-danger" />

            </div>
        </div>

        <asp:GridView
            ID="dgvReservas"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered">

            <Columns>
                <asp:BoundField DataField="FechaClase" HeaderText="Fecha" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>

        </asp:GridView>

    </div>

</asp:Content>