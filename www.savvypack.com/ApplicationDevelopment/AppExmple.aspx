<%@ Page Title="Internal Applications" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="AppExmple.aspx.vb" Inherits="ApplicationDevelopment_AppExmple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style="height:22px;width:97.5%;font-weight:bold;font-size:23px;text-align:center;margin-top:2px;margin-left:5px;color:#825f05;">
       Internal Applications 
     </div>
     <div id="Div1"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px;">  
          <table cellspacing="2" style="width:100%;" >
                 <tr>
                   <td colspan="2">
                       <div style="font-weight:bold;font-size:16px;text-align:left;text-decoration:underline;color:Black;font-family:Optima;">
                            Econ1
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                        
                                <table width="100%" cellpadding="2" border="0">
                                  <tr>
                                    <td valign="top">
                                        <asp:Image ID="imgE1" runat="server" ImageUrl="~/Images/Econ1Screen_S.gif" BorderColor="Black" BorderWidth="1"/>
                                    </td>

                                    <td valign="top" width="300px">
                                        <font face="Verdana">
                                             <font style="font-size:11px;">
                                             SavvyPack Corporation has designed and implemented a comprehensive tool brandnamed SavvyPack<sup>&reg;</sup>.  
                                             The tool is a true analytical tool for economic and environmental analysis.  
                                             The picture is of the Material & Structure screen, which is used to specify formulations.
                                        </font> </font>
                                    </td>
                                  </tr>

                                                                  
                                </table>
                             
				  </td>
                </tr>  
                                               
          </table>   
    </div>
</asp:Content>


