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

function AddNewFaculty() {

}