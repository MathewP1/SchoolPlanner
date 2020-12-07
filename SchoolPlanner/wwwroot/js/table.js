$(".dropdown-item").click(function () {
    var roomNumber = $(this).text();
    clearTable();
    $("#dropdownMenuLink").text(roomNumber);
    GetActivities(roomNumber);
});

$("td").click(function () {
    var content = $(this).text();
    if (content == "") {
        window.location.href = "SchoolActivities/Create/" + $(this).attr('id');
    } else {
        window.location.href = "SchoolActivities/Edit/" + $(this).attr('id');
    }
})

window.onload = function () {
    var roomNumber = $("#dropdownMenuLink").text();
    console.log(roomNumber);
    GetActivities(roomNumber);
};


function GetActivities(roomNumber) {
    $.get("SchoolActivities/GetActivities/" + roomNumber, function (response, status) {
        if (!response["activities"][0]) {
            return;
        } else {
            loadTable(response["activities"]);
        }
    });
}

function loadTable(data) {
    data.forEach(function (element, index, array) {
        var slot = element["slot"];
        $("#".concat(slot)).text(element["group"] + " " + element["class"] + " " + element["teacher"]);
    })
}

function clearTable() {
    $("td").each(function () {
        $(this).text("");
    })
}