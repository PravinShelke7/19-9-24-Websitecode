<%@ Page Title="Structure Comparison" Language="VB" MasterPageFile="~/Masters/StructAssistComp.master"
    AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_StandAssist_Assumptions_Extrusion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StructAssistCompContentPlaceHolder"
    runat="Server">
    <style>
        .divHeaderNew
        {
            position: absolute;
            margin-top: 0px;
            margin-left: 0px;
            text-align: left;
            vertical-align: top;
            width: 200px;
            z-index: 1;
        }
        .divContent2NEW
        {
            overflow: hidden;
            display: none;
            margin-top: 20px;
            border-bottom: solid 2px black;
            border-right: solid 2px black;
            border-left: solid 1px #E0E1E4;
            width: 197px;
            position: absolute;
            font-family: Optima;
            font-size: 10pt;
            background-color: White;
            margin-left: 10px;
            cursor: pointer;
        }
        
        
        .divHeaderNew2
        {
            position: absolute;
            margin-top: 0px;
            margin-left: 10px;
            text-align: left;
            vertical-align: top;
            width: 200px;
            z-index: 1;
        }
        
        .divHeaderNew3
        {
            position: absolute;
            margin-top: 0px;
            margin-left: 10px;
            text-align: left;
            vertical-align: top;
            width: 200px;
            z-index: 1;
        }
    </style>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV1.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px" style="text-align: left; margin-right: 350px; margin-left: 10px;">
        <tr align="left">
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Structure Comparison')"
                onmouseout="UnTip()">
                Structure Comparison
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
    <table style="text-align: left; margin-right: 350px;" width="840px">
        <tr>
            <td align="left" style="padding-right: 0px; width: 300px;">
                <div id="divHeader" class="divHeaderNew" onclick="toggleDiv('divContent', 'img1')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Structure Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img1" alt="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContent" class="divContent">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px; text-align: left;">
                        <%    
                            Try
                                
                                Dim CaseHide As New Integer
                                For CaseHide = 1 To DataCnt + 1
                                    Response.Write("<Input type='checkbox' id='chkBox_" & (CaseHide) & "' value='1' checked='true' onclick='showhideColumn (" & CStr(CaseHide) & ")'>")
                                    Response.Write("Structure#" & CaseDesp(CaseHide - 1) & "<br/>")
                                Next
                                      
                            Catch ex As Exception
                                      
                            End Try
                        %>
                    </div>
                </div>
            </td>
            <td align="left" style="width: 300px;">
                <div class="divHeaderNew2" onclick="toggleDiv('divContent2', 'img2')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Material Layer Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContent2" class="divContent2NEW">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px; text-align: left;">
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_EFFD" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_EFFD'); " />Effective
                        Date
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_M" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_M'); " />Material
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_GRADE" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_GRADE'); " />Grade
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_T" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_T'); " />Thickness
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_Weight" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_Weight'); " />Weight
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_OTRS" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_OTRS'); " />OTR
                        Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_OTRP" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_OTRP'); " />OTR
                        Pref.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_WVTRS" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_WVTRS'); " />WVTR
                        Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_WVTRP" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_WVTRP'); " />WVTR
                        Pref.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TS1S" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_TS1S'); " />Tensile
                        at Break MD Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TS1P" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_TS1P'); " />Tensile
                        at Break MD Pref.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TS2S" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_TS2S'); " />Tensile
                        at Break TD Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TS2P" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_TS2P'); " />Tensile
                        at Break TD Pref.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_SGS" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_SGS'); " />Specific
                        Gravity Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_SGP" checked="checked"
                            onclick="showhideALL('ctl00_StructAssistCompContentPlaceHolder_SGP'); " />Specific
                        Gravity Pref.
                        <br />
                        <b>Total</b>
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TThick" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TThick'); " />Total
                        Thickness
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TWeight" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TWeight'); " />Total
                        Weight
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TOTR" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TOTR'); " />Total
                        OTR
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TWVTR" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TWVTR'); " />Total
                        WVTR
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TTS1" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TTS1'); " />Total
                        Tensile Strength at Break MD
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TTS2" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TTS2'); " />Total
                        Tensile Strength at Break TD
                        <br />
                        <input type="checkbox" id="ctl00_StructAssistCompContentPlaceHolder_TSG" checked="checked"
                            onclick="showhideTotal('ctl00_StructAssistCompContentPlaceHolder_TSG'); " />Total
                        Specific Gravity
                        <br />
                    </div>
                </div>
            </td>
            <td style="width: 250px">
                <div class="divHeaderNew3">
                    <table>
                        <tr style="height: 20px;">
                            <td>
                                Column Width:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDWidth" runat="server" Text="300" CssClass="SmallTextBox"></asp:TextBox>
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
        <div id="PageSection1" style="text-align: left; width: 1200px;">
            <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2" Width="99%"
                Style="padding-left: 15px; padding-right: 2px;">
            </asp:Table>
            <br />
        </div>
    </div>
</asp:Content>
