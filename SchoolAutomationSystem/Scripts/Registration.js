function editClass(classId) {

    var url = window.location.href;
    url = url.substring(0, url.lastIndexOf("/Registration"));
   // alert("url");
    $.ajax({
        url: url + "/Registration/GetClassName",
        type: "POST",
        data: { ClassId: classId },

        success: function (result) {
            var a = result;
            $("#Model_className")[0].value = result;
            $("#Model_id")[0].value = classId;
            alert($("#Model_id")[0].value);
        },
        error: function (xhr, status, error) {

        }
    });
    return false;
}

function ValidateTextBoxRequired(element, message) {
    
    var text = element[0].value;
    
    if (text == "") {
        
        element.addClass("ValidationError");
        element.attr("title", message);
        return false;
    }
    else {
        element.removeClass("ValidationError");
        element.attr("title", "");
        return true;
    }
}
function ValidateDropDownBoxRequired(element, message) {

    var index = element[0].selectedIndex;
    
    if (index == 0) {

        element.addClass("ValidationError");
        element.attr("title", message);
        return false;
    }
    else {
        element.removeClass("ValidationError");
        element.attr("title", "");
        return true;
    }
}

function ValidateTextBoxAlphaRequired(element, message) {

    var text = element[0].value;
    var regex = /^[a-zA-Z ]*$/;
    if (!regex.test(text)) {

        element.addClass("ValidationError");
        element.attr("title", message);
        return false;
    }
    else {
        element.removeClass("ValidationError");
        element.attr("title", "");
        return true;
    }
}

function validateEmail(element, message) {

    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    var text = element[0].value;
    if (text != null || text != "") {

        if (!emailReg.test(text)) {

            element.addClass("ValidationError");
            element.attr("title", message);
            return false;

        } else {

            element.removeClass("ValidationError");
            element.attr("title", "");
            return true;
        }
    }
    else {
    return true;
    }
}
