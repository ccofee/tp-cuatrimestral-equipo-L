<%@ Page Title="Horarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Horarios.aspx.cs" Inherits="Presentacion.Horarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .validacion {
            color: red;
            font-size: 12px;
        }
    </style>

    <div class="container">

        <h2 class="mb-4">Horarios</h2>

        <div class="card mb-4">
            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-md-3">
                        <label>Día de la semana</label>
                        <asp:DropDownList ID="ddlDiaSemana" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Lunes" Value="1" />
                            <asp:ListItem Text="Martes" Value="2" />
                            <asp:ListItem Text="Miércoles" Value="3" />
                            <asp:ListItem Text="Jueves" Value="4" />
                            <asp:ListItem Text="Viernes" Value="5" />
                            <asp:ListItem Text="Sábado" Value="6" />
                            <asp:ListItem Text="Domingo" Value="7" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label>Hora inicio</label>
                        <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" TextMode="Time" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtHoraInicio" ErrorMessage="Requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label>Hora fin</label>
                        <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" TextMode="Time" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtHoraFin" ErrorMessage="Requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-3">
                        <label>Cupo máximo</label>
                        <asp:TextBox ID="txtCupoMaximo" runat="server" CssClass="form-control" TextMode="Number" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="txtCupoMaximo" ErrorMessage="Requerido" CssClass="validacion" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary me-2" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-warning me-2" OnClick="btnModificar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnEliminar_Click" />
                </div>

                <asp:Label ID="lblErrorHorario" runat="server" CssClass="validacion" Visible="false"></asp:Label>

            </div>
        </div>

        <asp:GridView ID="dgvHorarios"
            runat="server"
            AutoGenerateColumns="False"
            AutoGenerateSelectButton="true"
            DataKeyNames="Id"
            CssClass="table table-striped table-bordered"
            OnSelectedIndexChanged="dgvHorarios_SelectedIndexChanged">

            <Columns>
                <asp:BoundField DataField="DiaSemana" HeaderText="Día" />
                <asp:BoundField DataField="HoraInicio" HeaderText="Hora inicio" />
                <asp:BoundField DataField="HoraFin" HeaderText="Hora fin" />
                <asp:BoundField DataField="CupoMaximo" HeaderText="Cupo máximo" />
            </Columns>

        </asp:GridView>

    </div>
</asp:Content>
