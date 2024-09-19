function MakeVisible(id)
 {          
       
        objItemElement = document.getElementById(id)
        objItemElement.style.display = "inline"
        GetCaseDe();
       
        
      
        
  }
function MakeInVisible(id)
 {
        objItemElement = document.getElementById(id)
        objItemElement.style.display = "none"
        return false;
       
        
  }
  
function GetCaseDe()
{
    
    var combo1 = document.getElementById("PCases"); 
    var val = combo1.options[combo1.selectedIndex].text 
    var CaseDe = val.split("=");
    document.getElementById("txtCaseDe1").value = trimAll(CaseDe[1].replace("UNIQUE FEATURES",""));
    document.getElementById("txtCaseDe2").value = trimAll(CaseDe[2]);
}
  
  function GetCaseId()
  {
    
    objItemElement = document.getElementById("PCases")

    document.getElementById("txtCaseid").value = objItemElement.value
    
  
    
   
    if(document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0)
    {
        alert("PACKAGING FORMAT cannot be blank");
        return false;
    }
    else if(document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0)
    {
        alert("UNIQUE FEATURES cannot be blank");
        return false;
    }
    else
    {
        return confirm("Do you want to rename Case "+objItemElement.value+" ?");
        
    }
    
    return false;
    
    
  }
  
function trimAll(sString) 
    {
        while (sString.substring(0,1) == ' ')
        {
            sString = sString.substring(1, sString.length);
        }
        while (sString.substring(sString.length-1, sString.length) == ' ')
        {
            sString = sString.substring(0,sString.length-1);
        }
        return sString;
    }
