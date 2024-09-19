<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Video.aspx.vb" Inherits="InteractiveServices_Video" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color:#e4e4e7;">
    <form id="form1" runat="server">
    <center>
        <div>
            <span style="width:100%; text-align: center;font-size:25px;font-family:Optima;font-weight:bold;">
                Structure Assistant™ at a Glance </span>
        </div>
        <div style="height:10px;">
        </div>
        <div>
            <video autoplay width="1100" height="648" controls preload="none">
    <!-- MP4 must be first for iPad! -->
    <source src="../Video/Ar54vC88Uj21/StructureAssistant/Captivate/StructureAssistantR17.mp4" type="video/mp4" />   
    <!-- fallback to Flash: -->
    <object  width="1100" height="670" type="application/x-shockwave-flash" data="../Video/Ar54vC88Uj21/StructureAssistant/Captivate/StructureAssistantR17.swf">
              <param name="movie" value="../Video/Ar54vC88Uj21/StructureAssistant/Captivate/StructureAssistantR17.swf" />
			  <param name="allowFullScreen" value="true" />
		<param name="wmode" value="transparent" />
      
    </object>
</video>
        </div>
    </center>
    </form>
</body>
</html>
