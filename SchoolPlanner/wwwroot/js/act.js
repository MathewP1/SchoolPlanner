window.onload = function () {

    $.get("SchoolActivities/GetData", function (response, status) {
        response["rooms"].forEach(function (element, index, array) {
            $("#inputRoom").append($('<option>', element["id"])
                .text(element["roomNumber"]));
        });

    });


    

};