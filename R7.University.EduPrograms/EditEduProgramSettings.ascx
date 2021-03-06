﻿<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="EditEduProgramSettings.ascx.cs" Inherits="R7.University.EduPrograms.EditEduProgramSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/css/admin.css" Priority="200" />
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/css/dnn-ac-combobox.css" />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/MVC/R7.University/R7.University/js/dnn-ac-combobox.js" />

<div class="dnnForm dnnClear">
	<asp:Panel id="panelGeneralSettings" runat="server">
        <h2 class="dnnFormSectionHead"><a href="#"><%: LocalizeString ("GeneralSettings.Section") %></a></h2>
        <fieldset>
            <div class="dnnFormItem">
                <dnn:Label id="labelEduLevel" runat="server" ControlName="comboEduLevel" />
                <asp:DropDownList id="comboEduLevel" runat="server" 
                    AutoPostBack="true"
                    OnSelectedIndexChanged="comboEduLevel_SelectedIndexChanged"
                    DataValueField="EduLevelID"
                    DataTextField="Title" />
            </div>
            <div class="dnnFormItem">
                <dnn:Label id="labelEduProgram" runat="server" ControlName="comboEduProgram" />
                <asp:DropDownList id="comboEduProgram" runat="server" CssClass="dnn-ac-combobox"
                    DataValueField="Value"
                    DataTextField="Text" />
            </div>
        </fieldset>
	</asp:Panel>	
	<h2 class="dnnFormSectionHead"><a href="#"><%: LocalizeString ("DisplaySettings.Section") %></a></h2>
    <fieldset>
		<div class="dnnFormItem">
            <dnn:Label id="labelAutoTitle" runat="server" ControlName="checkAutoTitle" />
            <asp:CheckBox id="checkAutoTitle" runat="server" />
        </div>
    </fieldset> 
</div>

<script type="text/javascript">
(function($, Sys) {
    function setupModule() {
        dnnAcCombobox_Init($);
        $(".dnn-ac-combobox").combobox();
    };
    $(document).ready(function() {
        setupModule();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
            setupModule();
        });
    });
} (jQuery, window.Sys));
</script>