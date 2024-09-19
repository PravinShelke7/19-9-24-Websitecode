<%@ Page Title="S3-Energy Assumptions" Language="VB" MasterPageFile="~/Masters/Sustain3.master"
    AutoEventWireup="false" CodeFile="EnergyIn.aspx.vb" Inherits="Pages_Sustain3_Assumptions_EnergyIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" runat="Server">

   

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

    <table width="840px">
        <tr align="left">
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Energy Assumptions')"
                onmouseout="UnTip()">
                Energy Assumptions
            </td>
            <td style="width: 23%" class="PageSHeading">
                <table>
                    <tr>
                        <td>
                            Comparison ID:
                        </td>
                        <td>
                            <asp:Label ID="lblAID" CssClass="LableFonts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%" class="PageSHeading">
                <table>
                    <tr>
                        <td>
                            Description:
                        </td>
                        <td>
                            <asp:Label ID="lblAdes" CssClass="LableFonts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                <div id="divHeader" class="divHeader" onclick="toggleDiv('divContent', 'img1')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Case Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img1" alt="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContent" class="divContent">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px">
                        <%    
                                  Try
                                
                                      Dim CaseHide As New Integer
                                      For CaseHide = 1 To DataCnt + 1
                                    Response.Write("<Input type='checkbox' id='chkBox_" & (CaseHide) & "' value='1' checked='true' onclick='showhideColumn (" & CStr(CaseHide) & ")'>")
                                          Response.Write("Case" & CaseDesp(CaseHide - 1) & "<br/>")
                                      Next
                                      
                                  Catch ex As Exception
                                      
                                  End Try
                        %>
                    </div>
                </div>
            </td>
            <td>
                <div id="divHeader2" class="divHeader2" onclick="toggleDiv('divContent2', 'img2')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Layer Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt="" />
                            </td>
                        </tr>
                     
                    </table>
                </div>
                <div id="divContent2" class="divContent2">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px">
                        <b>Energy Ratios</b>
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EESP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_EESP'); " />Electricity
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_NESP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_NESP'); " />Natural
                        Gas
                        <br />
                        <b>Conversion Factors</b>
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CA" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CA'); " />Mj Per Kwh
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CB" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CB'); " />Mj Per Cubic ft
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CC" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CC'); " />Co2kg Per
                        Kwh
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CD" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CD'); " />Co2kg Cubic ft
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CE" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CE'); " />gallon Per Kwh
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CF" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CF'); " />gallon Per Cubicft
                        <br />
                        <b>Energy Requirements</b>
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ERPROD" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ERPROD'); " />Production
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ERWARE" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ERWARE'); " />Warehouse
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EROFF" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_EROFF'); " />Office
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ERSUPP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ERSUPP'); " />Support
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EATOT" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_EATOT'); " />Total
                        <br />
                        <b>Energy in Cradle Equi.</b>
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECEPROD" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ECEPROD'); " />Production
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECEWARE" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ECEWARE'); " />Warehouse
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECEOFF" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ECEOFF'); " />Office
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECESUPP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ECESUPP'); " />Support
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EBTOT" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_EBTOT'); " />Total
                        <br />
                        <b>CO2 in Cradle Equi.</b>
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GCEPROD" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_GCEPROD'); " />Production
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GCEWARE" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_GCEWARE'); " />Warehouse
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GCEOFF" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_GCEOFF'); " />Office
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GCESUPP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_GCESUPP'); " />Support
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECTOT" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_ECTOT'); " />Total
                        <br />
                            <b>Water in Cradle Equi.</b>
                                                  <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATPROD" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_WATPROD'); " />Production
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATWARE" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_WATWARE'); " />Warehouse
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATOFF" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_WATOFF'); " />Office
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATSUPP" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_WATSUPP'); " />Support
                        <br />
                        <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EDTOT" checked="checked"
                            onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_EDTOT'); " />Total
                    </div>
                </div>
            </td>
            <td>
                <div class="divHeader3">
                    <table>
                        <tr style="height: 20px;">
                            <td>
                                Column Width:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDWidth" runat="server" Text="270" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnWidthSet" runat="server" Text="Set" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="ContentPagemargin">
        <div id="PageSection1" style="text-align: left">
         <a href="AdditionalEnergyInfo.aspx" class="Link" target="_blank" style="font-weight: bold;
                                    color: red;" >Addtional Energy Assumptions</a>
            <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
        </div>
    </div>
</asp:Content>
