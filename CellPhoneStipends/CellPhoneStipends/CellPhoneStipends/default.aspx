<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SiteMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CellPhoneStipends._default" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <link href="Content/DefaultStyles.css" rel="stylesheet" />
    <script src="<%= Page.ResolveClientUrl("~/Scripts/DefaultScripts.js") %>"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divCellPhoneStipend" runat="server" clientidmode="Static">
        <fieldset>
            <div class="section">
               Employee Mobile Phone Stipend Request Form
            </div>
            <div class="inner-wrap">
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            Action: <span class="ValidationError">*</span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAction"
                                        CssClass="ValidationError"
                                        Text="Required"
                                        EnableClientScript="True"
                                        InitialValue="Select...">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList runat="server" ID="ddlAction" 
                                DataValueField="Type"
                                DataTextField="Type"/>
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            Employee Name: <span class="ValidationError">*</span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmployeeName"
                                        CssClass="ValidationError"
                                        Text="Required"
                                        EnableClientScript="True">
                            </asp:RequiredFieldValidator>
                            <input type="text" id="txtEmployeeName" runat="server"
                                   placeholder="Employee Name"/>
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            Department/Lab:
                            <input type="text" id="txtDepartmentLab" runat="server"
                                   placeholder="Department/Lab"/>
                        </label>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-4">
                        <label>
                            Your Supervisor's Email Address: <span class="ValidationError">*</span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSupervisorEmail"
                                        CssClass="ValidationError"
                                        Text="Required"
                                        EnableClientScript="True" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="valRegtxtSupervisorEmail" runat="server"
                                CssClass="ValidationError"
                                ControlToValidate="txtSupervisorEmail" ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$"
                                Text="Invalid Email Format"></asp:RegularExpressionValidator>
                            <input type="text" id="txtSupervisorEmail" runat="server" 
                                clientidmode="Static"
                                placeholder="Supervisor's Email"/>
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                         <label>
                            Monthly Stipend Level: <span class="ValidationError">*</span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMonthlyStipendLevel"
                                CssClass="ValidationError"
                                Text="Required"
                                EnableClientScript="True"
                                InitialValue="Select...">
                            </asp:RequiredFieldValidator>
                            <asp:DropDownList runat="server" ID="ddlMonthlyStipendLevel" 
                                DataValueField="StipendLevel"
                                DataTextField="StipendLevel"
                                ClientIDMode="Static"
                                />
                        </label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            Effective Date of Change: <span class="ValidationError">*</span>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEffectiveDate"
                                        CssClass="ValidationError"
                                        Text="Required"
                                        EnableClientScript="True">
                            </asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEffectiveDate" runat="server" ClientIDMode="Static"/>
                        </label>
                    </div>
                </div>
            </div>
        </fieldset>
        <div class="row">
            <div class="col-md-1">
                <asp:Button runat="server" id="btnSave"
                            ClientIDMode="Static"
                            Text="Submit" 
                            OnClick="btnSave_Click"
                            OnClientClick="HideCellphoneStipendDiv()"
                            Enabled="<%#CanSave() %>"/>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnCancel" runat="server"
                            Text="Clear Entries"
                            CausesValidation="False" OnClick="btnCancel_Click"/>
            </div>
        </div>
    </div>
  
    <div id="divPleaseWait" title="Please Wait" 
        runat="server" 
        clientidmode="Static"
        style="width:600px; height:100%; text-align:center;margin:auto;">
        <div style="margin:auto">
            <h2 style="font-size: 24px; font-weight:bolder; margin-top:50px;">
                Please wait while your cellphone stipend request is forwarded to human resources.
            </h2>

            <img src="Content/Images/VAI_Logo.jpg" style="margin-top: 20px; height: 182px; width: 155px;" alt="Van Andel Institute Logo"/>
        </div>
    </div>
</asp:Content>
