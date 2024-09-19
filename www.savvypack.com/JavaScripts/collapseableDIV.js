//collapseable div script
//***********************
//** Matthew M. Osborn **
//**  www.osbornm.com  **
//***********************


//Configuration section
//********************************************************
var ExpandImageSrc = '../../../Images/down.png'; //image location to display when Div is collapsed
var CollapseImageSrc = '../../../Images/up.png'; //image location to display when Div is Expanded
var speed = 10; //how often the div refreshes to the new height
var incriment = 10; //each time hte div refreshes height will be increased or deceased by this amount
//********************************************************


//Code
//********************************************************
var iTimer;


function toggleDiv(divToShow, imgID) {
    var help = document.getElementById(divToShow);
    if (help.style.display != "block") {
	
		if(imgID){
			document.getElementById(imgID).setAttribute('src',CollapseImageSrc);
		}
		var calcHeight = showDiv(divToShow);
		
		Expand(divToShow, calcHeight);
	}
	else {
		if(imgID){
			document.getElementById(imgID).setAttribute('src',ExpandImageSrc);

}
        var calcHeight = showDiv(divToShow);

        collapse(divToShow, calcHeight);
	}
}
function Expand(divName, calcHeight) {
    
	var help = document.getElementById(divName);
	var height = help.offsetHeight;

	if (height < calcHeight) {

	    help.style.height = height + incriment + "px";


	    iTimer = setTimeout("Expand('" + divName + "'," + calcHeight + ")", speed);
		
	}
	else{
		clearTimeout(iTimer);
	}	
}
function collapse(divName, calcHeight) {
    
	var help = document.getElementById(divName);
	var height = help.offsetHeight;
	
	
	if(height > incriment){
	    help.style.height = height - incriment + "px";

	    iTimer = setTimeout("collapse('" + divName + "'," + calcHeight + ")", speed);
	}
	else{
		clearTimeout(iTimer);
		help.style.height = calcHeight;
		help.style.display = "none";
	}	
}
function showDiv(divName) {
    var calcHeight;
    var div = document.getElementById(divName);
	div.style.display = "block";
	calcHeight = div.offsetHeight - 2;
	div.style.height = "520px";
	return calcHeight

} 
//*******************************************************