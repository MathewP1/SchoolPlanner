$(function () {
    //bind event
    $(".dropdown-menu a").click(function () {
        var selText = $(this).text();
        console.log(selText);
        $(this).parents('.dropdown').find('.dropdown-toggle').html(selText);
    });

    //trigger event
    $('.dropdown-menu a').first().trigger('click');
});