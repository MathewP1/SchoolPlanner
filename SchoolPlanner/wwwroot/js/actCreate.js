window.onload = function () {
    var baseUrl = window.location.origin;
    $.get(baseUrl + "/SchoolActivities/GetData", function (response, status) {
        // clear rooms here
        response["rooms"].forEach(function (element, index, array) {
            if (element["roomNumber"] != $("#inputRoom option:selected").text()) {
                $("#inputRoom").append($('<option>').text(element["roomNumber"]));
            }
        });
        response["groups"].forEach(function (element, index, array) {
            if (element["groupName"] != $("#inputGroup option:selected").text()) {
                $("#inputGroup").append($('<option>').text(element["groupName"]));
            }
        });
        response["classes"].forEach(function (element, index, array) {
            if (element["className"] != $("#inputClass option:selected").text()) {
                $("#inputClass").append($('<option>').text(element["className"]));
            }
        });
        response["teachers"].forEach(function (element, index, array) {
            if (element["teacherName"] != $("#inputTeacher option:selected").text()) {
                $("#inputTeacher").append($('<option>').text(element["teacherName"]));
            }
        });

        var slot = window.location.href.substring(window.location.href.lastIndexOf('/') + 1);
        console.log(slot);
        var i = Math.floor(slot / 5);
        var time;
        switch (i) {
            case 0:
                time = "8:00-8:45";
                break;
            case 1:
                time = "8:55-9:40";
                break;
            case 2:
                time = "9:50-10:35";
                break;
            case 3:
                time = "10:45-11:30";
                break;
            case 4:
                time = "11:40-12:25";
                break;
            case 5:
                time = "12:35-13:20";
                break;
            case 6:
                time = "13:30-14:15";
                break;
            case 7:
                time = "14:25-15:10";
                break;
            case 8:
                time = "15:20-16:05";
                break;
        }
        $("#inputSlot").append($('<option>').text(time));
    });

    $("button_save").click(function () {

    });


};

