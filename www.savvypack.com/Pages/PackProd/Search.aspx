<%@ Page Language="VB" MasterPageFile="~/Masters/PackProd.master" AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Pages_PackProd_Search" title="Search Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PackProdContentPlaceHolder" Runat="Server">
<script type="text/JavaScript">
          
            
      function OpenNewWindow(Page,Title) {

            var width = 800;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'NewWindowComparison' + Title, params);
            

        }
        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 350;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            return false;

        }
        
         function ShowPopWindowState() {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 350;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var CountryId = document.getElementById('<%= ddlCountry.ClientID%>').value
            var Page = "PopUp/GetState.aspx?Id=ddlState&CountryId="+CountryId+"";
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            return false;

        }
</script>
    <div>
      <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
            <br />
              <div class="PageHeading" id="divMainHeading">
                       <table>
                          <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="Server" CssClass="LongTextBoxSearch" style="text-align:left;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonWMarigin" Text="Keyword Search" style="height:22px;"  /> 
                            </td>
                          </tr>
                       </table>
               </div>
        
            </td>
        </tr>
              
    
        <tr>
            <td>
                <div style="text-align:left;display:inline;" id="dvAddUser" runat="Server"  >
                    <table width="100%" style="text-align:left;">
                        <tr>
                           <td>
                             <asp:Label ID="lblError" runat="Server" ></asp:Label>
                           </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="700px" cellpadding="1px" cellspacing ="1px">
                                
                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;text-align:center" colspan="2">                                                      
                                                            Logic Search Criteria 
                                                    </td>
                                    </tr>
                                     <tr class="AlterNateColor2" >
                                        <td style="width:100%;text-align:left;" colspan="2">                                                                     
                                        </td> 
                                    </tr>
                                    <tr class="AlterNateColor1" style="width:100%">
                                        <td style="width:25%;text-align:right; height: 20px;">
                                            <asp:Label ID="lblComp" runat="server"  text="Company:" CssClass="MediumLabel"></asp:Label>
                                        </td>
                                        <td style="width:45%;text-align:left; height: 20px;">
                                            <asp:DropDownList ID="ddlCompany" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                            <asp:ImageButton ID="btnCompSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Company" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCompany.aspx?Id=ddlCompany');" />
                                        </td>
                                       
                                    </tr>
                                <tr class="AlterNateColor2" style="width:100%">
                                        <td style="width:25%;text-align:right;">
                                          <table cellpadding="0" cellspacing="0">
                                          <tr>
                                            <td>
                                                <asp:Label ID="lblState" runat="server"  text="Country:" CssClass="MediumLabel"></asp:Label>
                                            </td>
                                            <td>
                                            
                                                       <asp:DropDownList ID="ddlCountry" runat="Server" Width="130px"  CssClass="DropDownCon" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:ImageButton ID="ImageButton1" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Country" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCountries.aspx?Id=ddlCountry');" />
                                              
                                            </td>
                                            <td style="padding-left:10px">
                                                <asp:Label ID="Label1" runat="server"  text="State:" CssClass="MediumLabel"></asp:Label>
                                            </td>
                                          </tr>
                                          </table>
                                             
                                        </td>
                                        <td style="width:45%;text-align:left">
                                            
                                                  <asp:DropDownList ID="ddlState" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                                  <asp:ImageButton ID="btnStSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search State" Height="19px" OnClientClick="return ShowPopWindowState();" />
         
                                        
                                           
                                        </td>
                                    </tr>
                                    
                                    <%--  <tr class="AlterNateColor1" style="width:100%">
                                        <td style="width:25%;text-align:right;">
                                            <asp:Label ID="lblState" runat="server"  text="State:" CssClass="MediumLabel"></asp:Label>
                                        </td>
                                        <td style="width:45%;text-align:left">
                                            <asp:DropDownList ID="ddlState" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                           <asp:ImageButton ID="btnStSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search State" Height="19px" OnClientClick="return ShowPopWindowState();" />
                                        </td>
                                    </tr>--%>
                                    
                                    <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:25%;text-align:right">
                                         <asp:Label ID="lblProductType" runat="server"  text="Product Type:" CssClass="MediumLabel"></asp:Label>   
                                        </td>
                                        <td style="width:45%;text-align:left">
                                            <asp:DropDownList ID="ddlProductType" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                         <asp:ImageButton ID="btnProdTSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Product Type" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlProductType');" />
                                        </td>
                                    </tr>
                                     <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:25%;text-align:right">
                                         <asp:Label ID="lblProdService" runat="server"  text="Product Services:" CssClass="MediumLabel"></asp:Label>   
                                        </td>
                                        <td style="width:45%;text-align:left">
                                            <asp:DropDownList ID="ddlProdService" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                          <asp:ImageButton ID="btnProdSerSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Product Service" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlProdService');" />
                                        </td>
                                    </tr>
                                       <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:25%;text-align:right">
                                          <asp:Label ID="lblProdDevCap" runat="server"  text="Product Development Capabilities:" CssClass="MediumLabel"></asp:Label>   
                                        </td>
                                        <td style="width:45%;text-align:left">
                                            <asp:DropDownList ID="ddlProdDevCap" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                        <asp:ImageButton ID="btnProdDevCapSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Product Development Capability" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlProdDevCap');" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:25%;text-align:right;">
                                            <asp:Label ID="lblProcCap" runat="server"  text="Processing Capabilities:" CssClass="MediumLabel"></asp:Label>
                                        </td>
                                        <td style="width:45%;text-align:left">
                                           <asp:DropDownList ID="ddlProcCap" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                           <asp:ImageButton ID="btnProcCapSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Processing Capabilities" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlProcCap');" />
                                        </td>
                                    </tr>
                                     <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:25%;text-align:right;">
                                            <asp:Label ID="lblMacSys" runat="server"  text="Machinery Systems:" CssClass="MediumLabel"></asp:Label>
                                        </td>
                                        <td style="width:45%;text-align:left">
                                              <asp:DropDownList ID="ddlMacSys" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                              <asp:ImageButton ID="btnMacSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Machinery Systems" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlMacSys');" />
                                        </td>
                                    </tr>
                                     <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:25%;text-align:right;">
                                            <asp:Label ID="lblRepCus" runat="server"  text="Representative Customers:" CssClass="MediumLabel"></asp:Label>
                                        </td>
                                        <td style="width:45%;text-align:left">
                                              <asp:DropDownList ID="ddlRepCus" runat="Server" Width="240px"  CssClass="DropDownCon"></asp:DropDownList>
                                                <asp:ImageButton ID="btnCusSearch" runat="Server"  ImageUrl="~/Images/search_icon.png" ToolTip="Search Customer" Height="19px" OnClientClick="return ShowPopWindow('PopUp/GetCategory.aspx?Id=ddlRepCus');" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" >
                                        <td style="width:25%;text-align:center ;">
                                                                                  
                                        </td> 
                                        <td style="width:45%;text-align:left">
                                          <asp:Button ID="btnLogSearch" runat="server" CssClass="ButtonWMarigin" Text="Logic Search"  /> 
                                        
                                        </td>
                                       
                                    </tr>
                                     <tr class="AlterNateColor2" >
                                        <td style="width:100%;text-align:left;" colspan="2">                                                                     
                                        </td> 
                                    </tr>
                                   
                                 
                                </table>
                                
                                </td>
                        </tr>
                        <tr>
                                      <td style="height:70px;">
                                      
                                        </td>  
                                    </tr>
                        <tr>
                                      <td style="height:70px;">
                                      
                                          &nbsp;</td>  
                                    </tr>
                    </table>
                </div>
            </td>
        
        </tr>
      
       
       
    </table>
    <div style="display:none">
        <asp:Button ID="btnState" runat="server" Text="Get States" />
    </div>
  </div>
</asp:Content>

