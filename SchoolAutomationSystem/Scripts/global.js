
/* -----------------------------------------------------------------------------------------------------------------------------------------
    * Filename:   global.js
	* Purpose:    Global JS file
	* Date        Dec-2013
    * Project:    iSchool
	* Author:     
------------------------------------------------------------------------------------------------------------------------------------------ */

/*form validation*/
$("input.mandatory").focusout(function() {
var bool=$(this).hasClass("datepicker").toString();
var email = $(".email-val").val();
var mob =$(".mob-val").val();
var user=$(".user").val();
if($(this).val()=="" && bool=="false")
{
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".error-msg").show();
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".err-msg").hide();
}
else if(bool=="false")
{
$(this).closest(".form-group").removeClass("error-field").addClass("has-success").children(".error-msg").hide();
}
if($(this).val()=="" && bool=="true")
{
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".error-msg").show();
}
/*email*/
else if($(".email-val").val()!="" && IsEmail(email)==false)
{
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".err-msg").show();
return false;
}
else if($(".email-val").val()!="" && IsEmail(email)==true)
{
$(this).closest(".form-group").removeClass("error-field").addClass("has-success").children(".err-msg").hide();
}
/*mobile*/
if($(".mob-val").val()!="" && IsMobile(mob)==false )
{
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".err-msg").show();
return false;
}
else if($(".mob-val").val()!="" && IsMob(mob)==true)
{
$(this).closest(".form-group").removeClass("error-field").addClass("has-success").children(".err-msg").hide();
}
/*name(alphabets only)*/
if(user!="" && IsAlpha(user)==false )
{
$(this).closest(".form-group").removeClass("has-success").addClass("error-field").children(".err-msg").show();
return false;
}
else if(user!="" && IsAlpha(user)==true)
{
$(this).closest(".form-group").removeClass("error-field").addClass("has-success").children(".err-msg").hide();
}
});
/*Validation for email*/
function IsEmail(email) {
var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
if(!regex.test(email)) {
return false;
}else{
return true;
}
}
/*Validation for mobile number*/
function IsMobile(mob) {
var pattern = /^\d{10}$/;
if (pattern.test(mob)) {
return true;
}
return false;
}
/*Validation for name(alphabets)*/
function IsAlpha(alpha) {
var regex = /^[a-zA-Z ]*$/;
if(regex.test(alpha)) {
return true;
}else
return false;
}

$('.nav li').click(function () {
    $('.nav li').removeClass("active");
    $(this).addClass("active");
});