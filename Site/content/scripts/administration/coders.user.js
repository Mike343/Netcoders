$(document).ready(function () {
    $("select.normal").selectmenu({
        style: 'dropdown',
        width: 250,
        maxHeight: 200
    });

    $("select.countries").selectmenu({
        style: 'dropdown',
        width: 300,
        maxHeight: 200
    });

    $("select.timezones").selectmenu({
        style: 'dropdown',
        width: 376,
        maxHeight: 200
    });
});