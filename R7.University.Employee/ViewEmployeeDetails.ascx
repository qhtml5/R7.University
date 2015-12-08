﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployeeDetails.ascx.cs" Inherits="R7.University.Employee.ViewEmployeeDetails" %>
<%@ Register TagPrefix="dnn" TagName="jQuery" Src="~/Admin/Skins/jQuery.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:jQuery runat="server" jQueryUI="true" DnnjQueryPlugins="true" />
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/R7.University/R7.University.Employee/js/module.js" />
<script type="text/javascript">
	$(function() { $( "#employeeTabs_<%= ModuleId %>" ).dnnTabs( <%= (!IsPostBack)? "{selected: 0}" : "" %>  ); });
</script>
<asp:Panel id="panelEmployeeDetails" runat="server" CssClass="dnnForm dnnClear employeeDetails">
    <div class="employeeDetailsTable">
    	<asp:Image id="imagePhoto" runat="server" CssClass="_photo" />	
    	<div id="employeeTabs_<%= ModuleId %>" class="dnnForm dnnClear _tabs">
            <asp:Literal id="literalFullName" runat="server" />
    		<ul class="dnnAdminTabNav dnnClear">
    		    <li><a href="#employeeCommon"><%= LocalizeString("CommonTab.Text") %></a></li>
    			<li><asp:HyperLink id="linkExperience" runat="server" href="#employeeExperience"><%= LocalizeString("ExperienceTab.Text") %></asp:HyperLink></li>
    			<li><asp:HyperLink id="linkAchievements" runat="server" href="#employeeAchievements"><%= LocalizeString("AchievementsTab.Text") %></asp:HyperLink></li>
    			<li><asp:HyperLink id="linkDisciplines" runat="server" href="#employeeDisciplines"><%= LocalizeString("DisciplinesTab.Text") %></asp:HyperLink></li>
    			<li><asp:HyperLink id="linkAbout" runat="server" href="#employeeAbout"><%= LocalizeString("AboutTab.Text") %></asp:HyperLink></li>
    		</ul>
    		<div id="employeeCommon" class="_tab">
    			<asp:Label id="labelAcademicDegreeAndTitle" runat="server" />
    			<asp:Repeater id="repeaterPositions" runat="server" OnItemDataBound="repeaterPositions_ItemDataBound">
    				<HeaderTemplate><ul class="_positions"></HeaderTemplate>
    				<ItemTemplate>
    					<li>
    						<asp:Label id="labelPosition" runat="server" />
    						<asp:Label id="labelDivision" runat="server" />
    						<asp:HyperLink id="linkDivision" runat="server" />
    					</li>
    				</ItemTemplate>
    				<FooterTemplate></ul></FooterTemplate>
    			</asp:Repeater>
    			<div class="_section">
    				<asp:HyperLink id="linkEmail" runat="server" CssClass="email _email" />
    				<asp:HyperLink id="linkSecondaryEmail" runat="server" CssClass="email _email" />
    				<asp:HyperLink id="linkWebSite" runat="server" Target="_blank" CssClass="_website" />
                    <asp:HyperLink id="linkUserProfile" runat="server" resourcekey="VisitProfile.Text" CssClass="_userprofile more" />
    			</div>
                <div class="_section">
    				<asp:Label id="labelMessenger" runat="server" CssClass="_label" />
    			</div>
    			<div class="_section">
    				<asp:Label id="labelPhone" runat="server" CssClass="_label" />
    				<asp:Label id="labelFax" runat="server" CssClass="_label" />
    				<asp:Label id="labelCellPhone" runat="server" CssClass="_label" />
    			</div>
                <div class="_section">
    				<asp:Label id="labelWorkingPlaceAndHours" runat="server" CssClass="_label" />
    			</div>
    		</div>
    		<div id="employeeExperience" class="_tab">	
    			<asp:Label id="labelExperienceYears" runat="server" CssClass="_label" />
    			<div class="_section" style="margin-bottom:10px">
    				<asp:GridView id="gridExperience" runat="server" AutoGenerateColumns="true" CssClass="dnnGrid grid-experience"
    						OnRowDataBound="gridExperience_RowDataBound" GridLines="None">
    						<HeaderStyle CssClass="dnnGridHeader" horizontalalign="Left" />
    				        <RowStyle CssClass="dnnGridItem" horizontalalign="Left" />
    				        <AlternatingRowStyle CssClass="dnnGridAltItem" />
    				        <SelectedRowStyle CssClass="dnnFormError" />
    				        <FooterStyle CssClass="dnnGridFooter" />
    			    </asp:GridView>
    			</div>
    		</div>
    		<div id="employeeAchievements" class="_tab">
    			<div class="_section" style="margin-bottom:10px">
    				<asp:GridView id="gridAchievements" runat="server" AutoGenerateColumns="true" CssClass="dnnGrid grid-achievements"
    						OnRowDataBound="gridExperience_RowDataBound" GridLines="None">
    						<HeaderStyle CssClass="dnnGridHeader" horizontalalign="Left" />
    				        <RowStyle CssClass="dnnGridItem" horizontalalign="Left" />
    				        <AlternatingRowStyle CssClass="dnnGridAltItem" />
    				        <SelectedRowStyle CssClass="dnnFormError" />
    				        <FooterStyle CssClass="dnnGridFooter" />
    			    </asp:GridView>
    			</div>		
    		</div>
    		<div id="employeeDisciplines" class="_tab">
                <div class="_section">
                    <asp:GridView id="gridEduPrograms" runat="server" AutoGenerateColumns="false" CssClass="dnnGrid grid-eduprograms" GridLines="None">
                        <HeaderStyle CssClass="dnnGridHeader" horizontalalign="Left" />
                        <RowStyle CssClass="dnnGridItem" horizontalalign="Left" />
                        <AlternatingRowStyle CssClass="dnnGridAltItem" />
                        <SelectedRowStyle CssClass="dnnFormError" />
                        <FooterStyle CssClass="dnnGridFooter" />
                        <Columns>
                            <asp:BoundField DataField="EduProfileString" HeaderText="EduProfile" />
                            <asp:BoundField DataField="Disciplines" HeaderText="Disciplines" />
                        </Columns>
                    </asp:GridView>
                </div>
    			<asp:Literal id="litDisciplines" runat="server" />
    		</div>
    		<div id="employeeAbout" class="_tab">
    			<asp:Literal id="litAbout" runat="server" />
    		</div>
    	</div>
    </div>
    <ul class="dnnActions dnnClear">
        <li><asp:HyperLink id="linkReturn" runat="server" CssClass="dnnPrimaryAction" ResourceKey="cmdReturn" /></li>
        <li><asp:HyperLink id="linkVCard" runat="server" CssClass="dnnSecondaryAction" ResourceKey="VCard.Action" Visible="false" /></li>
        <li><asp:HyperLink id="linkBarcode" runat="server" resourcekey="Barcode.Action" 
            CssClass="dnnSecondaryAction" onclick="showEmployeeBarcodeDialog(this)" />
        </li>
        <li><asp:HyperLink id="linkEdit" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdEdit" Visible="false" /></li>
    </ul>
</asp:Panel>
<div class="dialog-employee-achievement-description" id="dialog-employee-achievement-description-<%= ModuleId %>"></div>
<div class="dialog-employee-barcode" id="dialog-employee-barcode-<%= ModuleId %>">
    <asp:Image id="imageBarcode" runat="server" Style="margin-top:10px" />
    <asp:Label runat="server" resourcekey="BarcodeScan.Text" 
        CssClass="dnnFormMessage" Style="margin-top:10px;margin-bottom:0" />
</div>