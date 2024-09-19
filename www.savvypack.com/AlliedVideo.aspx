<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AlliedVideo.aspx.vb" Inherits="AlliedVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Allied Video</title>
</head>
<body onload="form1.Button1.click()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <input type="button" id="Button1" name="Button1" onclick="return checkPlayVideo()"
        style="display: none" />
    
      <center>
       <div style="background-color: #F1F1F2;">
            <center>
        <div>
          <center>
            <table>
                <tr style="height:55px; width:100%; vertical-align: middle;">
                    <td align="center" colspan="1" style="width:100%; text-align: center;font-size:25px;font-family:Optima;font-weight:bold ">
                       <asp:Label ID="lblHeader" runat="server" Width="100%" ></asp:Label>
                    </td>
                </tr>
            </table>
            </center> 
        </div>
        <div id="dvAlliedVideo">
        </div>
    </center>
     </div> 
      </center> 
    
   

    <script type="text/javascript">

        function checkPlayVideo() {
            var id=document.getElementById('hdnCode').value;
           
            if(id=="vm1")
            {
            PageMethods.Play_VM1('vm1', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf1")
            {
            PageMethods.Play_VF1('vf1', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
            else if(id=="vm2")
            {
            PageMethods.Play_VM2('vm2', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf2")
            {
            PageMethods.Play_VF2('vf2', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
             else if(id=="vm3")
            {
            PageMethods.Play_VM3('vm3', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf3")
            {
            PageMethods.Play_VF3('vf3', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
            else if(id=="vm4")
            {
            PageMethods.Play_VM4('vm4', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf4")
            {
            PageMethods.Play_VF4('vf4', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
            
            else if(id=="vm5")
            {
            PageMethods.Play_VM5('vm5', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf5")
            {
            PageMethods.Play_VF5('vf5', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
            
            else if(id=="vm6")
            {
            PageMethods.Play_VM6('vm6', OnCaseManagerSucceeded, OnCaseManagerFailed);
            return false;
            }
            else if(id=="vf6")
            {
            PageMethods.Play_VF6('vf6', OnCaseManagerSucceeded, OnCaseManagerFailed);
            }
            
            else if(id=="vm7")
            {
 if(navigator.userAgent.indexOf("Chrome") != -1 )
    {

         PageMethods.Play_VM7_CHROME('vm7', OnCaseManagerSucceededCONM, OnCaseManagerFailed);
            return false;
    }
   else
{
 PageMethods.Play_VM7('vm7', OnCaseManagerSucceededCONM, OnCaseManagerFailed);
            return false;

}

           
            }
            else if(id=="vf7")
            {
            PageMethods.Play_VF7('vf7', OnCaseManagerSucceededCON, OnCaseManagerFailed);
            }
            
        }

        function OnCaseManagerSucceeded(result) {
            document.getElementById('dvAlliedVideo').innerHTML = "<embed width='640px' kioskmode='true' height='504px' style='background-color:#F1F1F2;' src='" + result + "' />";
        }
function OnCaseManagerSucceededCONM(result) {
            document.getElementById('dvAlliedVideo').innerHTML = "<embed width='1050px' kioskmode='true' height='820px' style='background-color:#F1F1F2;' src='" + result + "' />";
        }
  function OnCaseManagerSucceededCON(result) {
            document.getElementById('dvAlliedVideo').innerHTML = "<embed width='1025px' kioskmode='true' height='799px' style='background-color:#F1F1F2;' src='" + result + "' />";
        }

        function OnCaseManagerFailed(result) 
        {
            alert('Error in playing video,Please conatact at sales@savvypack.com!')
        }
         
    </script>

    <asp:HiddenField ID="hdnCode" runat="server" />
    </form>
</body>
</html>
