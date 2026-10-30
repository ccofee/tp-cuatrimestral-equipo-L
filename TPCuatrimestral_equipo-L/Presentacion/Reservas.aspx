<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Presentacion.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <h2 class="mb-4">Reservas de la semana</h2>

        <div class="card mb-4">
            <div class="card-body">

                <div class="row mb-3">

                    <div class="col-md-4">
                        <label>Alumno</label>
                        <asp:TextBox ID="txtAlumno" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-4">
                        <label>Horario</label>
                        <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Lunes 18:00 - 19:00" Value="1" />
                            <asp:ListItem Text="Miércoles 19:00 - 20:00" Value="2" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label>Fecha de clase</label>
                        <asp:TextBox ID="txtFechaClase" runat="server" CssClass="form-control" />
                    </div>

                </div>

                <asp:Button
                    ID="btnReservar"
                    runat="server"
                    Text="Reservar"
                    CssClass="btn btn-primary me-2" />

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